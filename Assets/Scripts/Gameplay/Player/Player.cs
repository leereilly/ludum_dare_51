using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class Player : GameEntity
    {
        public PlayerController input { private set; get; }

        [SerializeField] private WeaponController weaponController;

        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private float jumpPower = 2500f;
        [SerializeField] float shootCooldown = 0.15f;
        [SerializeField] float bulletSpeed = 10f;

        [SerializeField] private Bullet bullet;
        [SerializeField] public Transform bulletPos;

        private Rigidbody2D rb;
        private GroundCheck groundCheck;

        private float shotTimestamp = 0f;

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
            rb.AddForce(new Vector2(input.movement * movementSpeed * Time.deltaTime, 0));

            if (input.Jump() && groundCheck.isGrounded) rb.AddForce(new Vector2(0, jumpPower));

            if (input.shooting && shotTimestamp < Time.time - shootCooldown)
            {
                shotTimestamp = Time.time;
                Shoot();
            }
        }

        private void Shoot()
        {
            var spawnedBullet = Instantiate(bullet, bulletPos.position, default, null);
            spawnedBullet.Init(input.aimVector.normalized * bulletSpeed, IDamagable.DamageType.Player);
            spawnedBullet.Load();
        }
    }
}
