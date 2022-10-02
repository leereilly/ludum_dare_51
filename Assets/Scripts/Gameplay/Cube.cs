using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

namespace Helzinko
{
    public class Cube : GameEntity
    {
        [SerializeField] CinemachineImpulseSource landingImpulse;

        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Sprite landingSprite;

        [SerializeField] private LayerMask mask;
        //[SerializeField] private LayerMask lavaMask;

        [SerializeField] private float fallingSpeed;

        [SerializeField] private Effect dieEffect;
        [SerializeField] private Effect spawnEffect;

        public bool isBottomCube = false;

        private SpriteRenderer sr;

        public Vector2 targetPos { private set; get; }
        //public bool isMoving { private set; get; } = false;

        private bool firstLanding = false;

        [SerializeField] private bool initialSpawned = false;

        public bool touchedLava = false;

        private BoxCollider2D col;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();

            if(initialSpawned)
                sr.sprite = sprites[Random.Range(0, sprites.Length)];
            else sr.sprite = landingSprite;

            col = GetComponent<BoxCollider2D>();

            // TODO: this is temp until loading implementing
            this.Load();
        }

        public void Spawn()
        {
            firstLanding = true;

            col.size = new Vector2(col.size.x / 2, col.size.y);

            Movement();
        }

        private void Update()
        {
            Movement();

            if(targetPos.y < transform.position.y)
            {
                var step = fallingSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            }


            if (transform.position.y <= targetPos.y)
            {
                if (firstLanding)
                {
                    firstLanding = false;

                    col.size = new Vector2(col.size.x * 2, col.size.y);

                    if (spawnEffect)
                    {
                        Instantiate(spawnEffect, new Vector2(transform.position.x, GetComponent<Collider2D>().bounds.min.y + 0.25f), default, null);
                    }

                    landingImpulse.GenerateImpulse();

                    sr.sprite = sprites[Random.Range(0, sprites.Length)];

                    SoundManager.instance.PlayEffect(GameType.SoundTypes.cubeHitGround);
                }


            }
        }

        public override void TakeDamage(float amount, IDamagable.DamageType type, Vector2 point)
        {
            if (firstLanding) return;

            if (type == IDamagable.DamageType.Lava) return;
            else base.TakeDamage(amount, type, point);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (firstLanding)
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
            if (touchedLava)
            {
                return;
            }

            var ray = Physics2D.Raycast(transform.position, Vector2.down, 100f, mask);

            if (ray)
            {
                var cube = ray.transform.GetComponent<Cube>();
                if (cube)
                {
                    targetPos = new Vector2(ray.transform.position.x, ray.transform.position.y + 1);
                }
                else
                {
                    var lava = ray.transform.GetComponent<Lava>();
                    if (lava)
                    {
                        targetPos = new Vector2(transform.position.x, Mathf.Round(ray.transform.GetComponent<Collider2D>().bounds.max.y));
                    }
                }
            }
            //if (ray)
            //{
            //    var bottomCube = ray.transform.GetComponent<Cube>();

            //    if (bottomCube.isMoving) targetPos = new Vector2(transform.position.x, bottomCube.targetPos.y + 1f);
            //    /*else*/
            //    targetPos = new Vector2(ray.transform.position.x, ray.transform.position.y + 1);
            //}
            //else
            //{
            //    ray = Physics2D.Raycast(transform.position, Vector2.down, 100f, lavaMask);
            //    if (ray)
            //    {
            //        targetPos = new Vector2(transform.position.x, Mathf.Round(ray.transform.GetComponent<Collider2D>().bounds.max.y));
            //    }

            //}
        }

        public override void Kill(IDamagable.DamageType type)
        {
            if (dieEffect)
                Instantiate(dieEffect, transform.position, default, null);

            SoundManager.instance.PlayEffect(GameType.SoundTypes.cubeDie);

            base.Kill(type);
        }

        //private void CheckIfGrounded()
        //{
        //    var cubeRay = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, mask);
        //    var lavaRay = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, lavaMask);
        //    if (!cubeRay && !lavaRay)
        //    {
        //        Movement();
        //    }
        //}
    }
}
