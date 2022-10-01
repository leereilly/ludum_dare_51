using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class CubeCleaner : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Cube cube))
            {
                cube.Kill(IDamagable.DamageType.Lava);
            }
        }
    }
}
