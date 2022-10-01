using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class FlyingEnemy : Enemy
    {
        [SerializeField] private SpriteRenderer sr;

        [SerializeField] private Effect dieEffect;

        protected Vector2 dir = Vector2.right;
        [SerializeField] private float patrolSpeed;

        [SerializeField] private Bullet bullet;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float timeBetweenShots;
        [SerializeField] private float distanceToPlayer;

        private float timeSinceLastShot;

        private bool flyingToPlayer = true;

        private Player player;

        private void Start()
        {
            dir = Random.value < 0.5f ? Vector2.right : Vector2.left;

            sr.flipX = dir == Vector2.left ? true : false;
        }

        private void Awake()
        {
            player = Gameplay.instance.player;
        }

        private void Update()
        {
            if (flyingToPlayer) return;

            if(timeSinceLastShot >= timeBetweenShots)
            {
                Shoot();

                timeSinceLastShot = 0;
            }

            timeSinceLastShot += Time.deltaTime;
        }

        public void FixedUpdate()
        {
            if (flyingToPlayer)
            {
                if (!player) return;
                if (Mathf.Abs((player.transform.position - transform.position).y) <= distanceToPlayer) flyingToPlayer = false;
                rb.velocity = Vector2.down * patrolSpeed * Time.deltaTime;
            }
            else
            {
                rb.velocity = dir * patrolSpeed * Time.deltaTime;
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "wall")
            {
                dir *= -1;
                sr.flipX = dir == Vector2.left ? true : false;
            }
        }

        private void Shoot()
        {
            var spawnedBullet = Instantiate(bullet, transform.position, default, null);
            spawnedBullet.Init(Vector2.down * bulletSpeed, IDamagable.DamageType.Enemy);
            spawnedBullet.Load();
        }

        public override void Kill(IDamagable.DamageType type)
        {
            if (dieEffect)
                Instantiate(dieEffect, transform.position, default, null);

            base.Kill(type);
        }
    }
}
