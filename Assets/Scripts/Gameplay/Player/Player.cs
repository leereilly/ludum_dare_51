using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace Helzinko
{
    public class Player : GameEntity
    {
        public UnityEvent OnShoot;
        public UnityEvent OnJump;
        public UnityEvent OnDie;
        public UnityEvent OnDamage;

        public PlayerController input { private set; get; }

        [SerializeField] private WeaponController weaponController;

        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private float jumpPower = 2500f;
        [SerializeField] float shootCooldown = 0.15f;
        [SerializeField] float bulletSpeed = 10f;
        [SerializeField] float stunnedTime = 1f;

        [SerializeField] private Bullet bullet;
        [SerializeField] public Transform bulletPos;

        [SerializeField] private Effect dieEffect;

        private Rigidbody2D rb;
        private GroundCheck groundCheck;

        private float shotTimestamp = 0f;

        public bool stunned { private set; get; }
        private Tween stunTween;

        private void Awake()
        {
            input = GetComponent<PlayerController>();
            rb = GetComponent<Rigidbody2D>();
            groundCheck = GetComponent<GroundCheck>();

            weaponController.Init(this);

            // TODO: this is temp until loading implementing
            this.Load();
        }

        private void Update()
        {
            if (stunned) return;

            rb.AddForce(new Vector2(input.movement * movementSpeed * Time.deltaTime, 0));

            if (input.Jump() && groundCheck.isGrounded)
            {
                rb.AddForce(new Vector2(0, jumpPower));
                OnJump.Invoke();
            }

            if (input.shooting && shotTimestamp < Time.time - shootCooldown)
            {
                shotTimestamp = Time.time;
                OnShoot.Invoke();
                Shoot();
            }
        }

        private void Shoot()
        {
            SoundManager.instance.PlayEffect(GameType.SoundTypes.playerShoot, 0.2f);

            var spawnedBullet = Instantiate(bullet, bulletPos.position, default, null);
            spawnedBullet.Init(input.aimVector.normalized * bulletSpeed, IDamagable.DamageType.Player);
            spawnedBullet.Load();
        }

        public override void TakeDamage(float amount, IDamagable.DamageType type, Vector2 point)
        {
            if(type == IDamagable.DamageType.Lava)
            {
                base.TakeDamage(amount, type, point);
            }
            else
            {
                stunned = true;
                OnDamage.Invoke();
                stunTween?.Kill();
                stunTween = DOVirtual.DelayedCall(stunnedTime, () => stunned = false, false).SetTarget(gameObject);
            }

        }

        public override void Kill(IDamagable.DamageType type)
        {
            if (dieEffect)
                Instantiate(dieEffect, transform.position, default, null);

            SoundManager.instance.PlayEffect(GameType.SoundTypes.playerDie);

            OnDie.Invoke();

            base.Kill(type);
        }

        public void SetCursor(Transform cursor)
        {
            GetComponent<PlayerController>().cursor = cursor;
        }
    }
}
