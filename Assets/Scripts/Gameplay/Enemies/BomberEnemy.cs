using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class BomberEnemy : Enemy
    {
        [SerializeField] private SpriteRenderer sr;

        [SerializeField] private Effect dieEffect;

        [SerializeField] private float movementSpeed;

        private Player player;

        private void Start()
        {
            var enemySpawner = EnemySpawner.instance;
            if (enemySpawner) movementSpeed = enemySpawner.GetCurrentPool().bomberSpeed;
        }

        private void Awake()
        {
            player = Gameplay.instance.player;
        }

        public void FixedUpdate()
        {
            rb.velocity = Vector2.down * movementSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IDamagable damagable))
            {
                if(collision.TryGetComponent(out Cube cube))
                {
                    if(cube.firstLanding) Kill(IDamagable.DamageType.Enemy);
                    else
                    {
                        damagable.TakeDamage(1f, IDamagable.DamageType.Enemy, transform.position);
                        Kill(IDamagable.DamageType.Enemy);
                    }
                }
                else
                {
                    damagable.TakeDamage(1f, IDamagable.DamageType.Enemy, transform.position);
                    Kill(IDamagable.DamageType.Enemy);
                }
            }
        }

        public override void Kill(IDamagable.DamageType type)
        {
            if (dieEffect)
                Instantiate(dieEffect, transform.position, default, null);

            base.Kill(type);
        }
    }
}
