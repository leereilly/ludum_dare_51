using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class Enemy : GameEntity
    {
        [SerializeField] protected LayerMask wallMask;
        [SerializeField] protected Cube cube;
        [SerializeField] protected Rigidbody2D rb;
        public override void Kill(IDamagable.DamageType type)
        {
            EnemySpawner.instance.RemoveEnemy(this);

            if (type != IDamagable.DamageType.Enemy)
            {
                var xPos = Gameplay.instance.grid.GetClosestX(transform.position.x);

                var cubeToSpawn = Instantiate(cube, new Vector2(xPos, transform.position.y), default, null);
                cubeToSpawn.Spawn();
            }


            base.Kill(type);
        }

        public override void TakeDamage(float amount, IDamagable.DamageType type, Vector2 point)
        {
            SoundManager.instance.PlayEffect(GameType.SoundTypes.enemyHurt);

            base.TakeDamage(amount, type, point);
        }
    }
}
