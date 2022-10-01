using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class Enemy : GameEntity
    {
        [SerializeField] private Cube cube;

        public override void Kill(IDamagable.DamageType type)
        {
            var xPos = Gameplay.instance.grid.GetClosestX(transform.position.x);

            var cubeToSpawn = Instantiate(cube, new Vector2(xPos, transform.position.y), default, null);

            base.Kill(type);
        }
    }
}
