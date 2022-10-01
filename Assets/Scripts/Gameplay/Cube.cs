using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Helzinko
{
    public class Cube : GameEntity
    {
        [SerializeField] private LayerMask mask;
        [SerializeField] private LayerMask lavaMask;

        [SerializeField] private float fallingSpeed;

        private Collider2D col;
        private Rigidbody2D rb;

        public bool printThis = false;

        public bool isBottomCube = false;

        private Vector2 targetPos;
        private bool isMoving = false;

        private void Awake()
        {
            // TODO: this is temp until loading implementing
            this.Load();
        }

        public void Spawn()
        {
            this.Load();

            Movement();
        }

        private void Update()
        {
            if (isMoving)
            {
                var step = 10f * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

                if(transform.position.y <= targetPos.y)
                {
                    isMoving = false;
                    CheckIfGrounded();
                }
            }
        }

        private void Start()
        {
            col = GetComponent<Collider2D>();
            rb = GetComponent<Rigidbody2D>();
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
                targetPos = new Vector2(ray.transform.position.x, ray.transform.position.y + 1);
            }
            else
            {
                ray = Physics2D.Raycast(transform.position, Vector2.down, 100f, lavaMask);
                if (ray)
                {
                    print(ray.transform.GetComponent<BoxCollider2D>().bounds.max.y);
                    targetPos = new Vector2(transform.position.x, Mathf.Round(ray.transform.GetComponent<BoxCollider2D>().bounds.max.y));
                }

                else targetPos = new Vector2(transform.position.x, transform.position.y - 100f);
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
    }
}
