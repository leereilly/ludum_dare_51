using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;
using System.Linq;

namespace Helzinko
{
    public class Bullet : MonoBehaviour, ILoadable
    {
        [SerializeField] private float time;
        [SerializeField] private Effect deathEffect;

        Tween killTween;

        protected Rigidbody2D rb;
        protected float damage;
        protected IDamagable.DamageType damageType;

        private bool collided = false;

        public virtual void Init(Vector2 velocity, IDamagable.DamageType damageType, float damage = 1)
        {
            rb = GetComponent<Rigidbody2D>();

            this.damage = damage;
            this.damageType = damageType;
            rb.velocity = velocity;
        }

        public void OnLoad()
        {
            killTween = DOVirtual.DelayedCall(time, () => this.Unload(), false).SetTarget(gameObject);
        }

        public void OnUnload()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collided) return;

            if (collision.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(damage, damageType, transform.position);
            }

            if(deathEffect)
                Instantiate(deathEffect, transform.position, default, null);

            collided = true;
            this.Unload();
        }
    }
}
