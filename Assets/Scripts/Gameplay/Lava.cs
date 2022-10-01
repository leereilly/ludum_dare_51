using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class Lava : MonoBehaviour
    {
        [SerializeField] private float damage = 100f;
        [SerializeField] private float waveRiseSpeed = 1f;
        [SerializeField] private float waveRiseHeigth = 1.1f;

        private float targetHeight;

        private float traveledTime = 0f;

        private void Start()
        {
            targetHeight = transform.position.y;
        }

        public void MoveUp()
        {
            targetHeight += waveRiseHeigth;
            traveledTime = 0f;
        }

        private void Update()
        {
            traveledTime += Time.deltaTime / waveRiseSpeed;
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, targetHeight), traveledTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(damage, IDamagable.DamageType.Lava, collision.transform.position);
            }
        }
    }
}
