using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Helzinko
{
    public class Cube : GameEntity
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Sprite landingSprite;

        [SerializeField] private LayerMask mask;
        [SerializeField] private LayerMask lavaMask;

        [SerializeField] private float fallingSpeed;

        public bool isBottomCube = false;

        private SpriteRenderer sr;

        public Vector2 targetPos { private set; get; }
        public bool isMoving { private set; get; } = false;

        private bool firstLanding = false;

        [SerializeField] private bool initialSpawned = false;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();

            if(initialSpawned)
                sr.sprite = sprites[Random.Range(0, sprites.Length)];
            else sr.sprite = landingSprite;

            // TODO: this is temp until loading implementing
            this.Load();

            Gameplay.instance.grid.OnCubeDestroy.AddListener(Movement);
        }

        public void Spawn()
        {
            firstLanding = true;

            Movement();
        }

        private void Update()
        {
            if (isMoving)
            {
                var step = fallingSpeed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

                if(transform.position.y <= targetPos.y)
                {
                    if (firstLanding)
                    {
                        firstLanding = false;

                        sr.sprite = sprites[Random.Range(0, sprites.Length)];
                    }

                    isMoving = false;
                    CheckIfGrounded();
                }
            }
        }

        public override void TakeDamage(float amount, IDamagable.DamageType type, Vector2 point)
        {
            if (isMoving) return;

            if (type == IDamagable.DamageType.Lava) return;
            else base.TakeDamage(amount, type, point);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (isMoving)
            {
                if(collision.transform.TryGetComponent(out Player player))
                {
                    player.TakeDamage(1f, IDamagable.DamageType.Cube, transform.position);
                    Kill(IDamagable.DamageType.Player);
                }
            }
        }

        private void Movement()
        {
            var ray = Physics2D.Raycast(transform.position, Vector2.down, 100f, mask);
            if (ray)
            {
                var bottomCube = ray.transform.GetComponent<Cube>();

                if (bottomCube.isMoving) targetPos = new Vector2(transform.position.x, bottomCube.targetPos.y + 1f);
                else targetPos = new Vector2(ray.transform.position.x, ray.transform.position.y + 1);
            }
            else
            {
                ray = Physics2D.Raycast(transform.position, Vector2.down, 100f, lavaMask);
                if (ray)
                {
                    targetPos = new Vector2(transform.position.x, Mathf.Round(ray.transform.GetComponent<Collider2D>().bounds.max.y));
                }

                //else targetPos = new Vector2(transform.position.x, transform.position.y - 100f);
            }

            isMoving = true;
        }

        private void CheckIfGrounded()
        {
            var cubeRay = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, mask);
            var lavaRay = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, lavaMask);
            if (!cubeRay && !lavaRay)
            {
                Movement();
            }
        }

        public override void Kill(IDamagable.DamageType type)
        {
            Gameplay.instance.grid.OnCubeDestroy.Invoke();

            base.Kill(type);
        }
    }
}
