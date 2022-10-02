using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class PlayerVisual : MonoBehaviour
    {
        private const string shootingAnim = "shoot";
        private const string runningString = "run";
        private const string jumpAnim = "jump";
        private const float flipAngle = 90f;

        [SerializeField] private Animator anim;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] CinemachineImpulseSource shootImpulse;
        [SerializeField] CinemachineImpulseSource damageImpulse;

        [SerializeField] Effect muzzleEffect;

        private Player player;
        private PlayerController input;
        private Rigidbody2D rb;
        private GroundCheck groundCheck;

        private void Awake()
        {
            player = GetComponent<Player>();
            input = GetComponent<PlayerController>();
            rb = GetComponent<Rigidbody2D>();
            groundCheck = GetComponent<GroundCheck>();
        }

        private void Start()
        {
            player.OnShoot.AddListener(Shoot);
            player.OnJump.AddListener(Jump);
            player.OnDamage.AddListener(DamageTaken);
        }

        private void Update()
        {
            bool playerMoving = input.movement != 0;
            anim.SetBool(runningString, playerMoving && rb.velocity.magnitude != 0 && !player.stunned);
            anim.SetBool(jumpAnim, !groundCheck.isGrounded);

            CharacterRotation();
        }

        private void CharacterRotation()
        {
            Vector3 difference = player.input.aimVector;
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            spriteRenderer.flipX = rotation_z > flipAngle || rotation_z < -flipAngle ? true : false;
        }

        private void Jump()
        {
            anim.SetTrigger(jumpAnim);
            SoundManager.instance.PlayEffect(GameType.SoundTypes.playerJump);
        }

        private void Shoot()
        {
            Instantiate(muzzleEffect, player.bulletPos.position, default, transform);
            shootImpulse.GenerateImpulse();
        }

        private void DamageTaken()
        {
            damageImpulse.GenerateImpulse();
        }
    }
}
