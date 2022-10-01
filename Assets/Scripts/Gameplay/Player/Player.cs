using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class Player : GameEntity
    {
        public PlayerController input { private set; get; }

        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private float jumpPower = 2500f;

        private Rigidbody2D rb;
        private GroundCheck groundCheck;

        private void Awake()
        {
            input = GetComponent<PlayerController>();
            rb = GetComponent<Rigidbody2D>();
            groundCheck = GetComponent<GroundCheck>();
        }

        private void Update()
        {
            rb.AddForce(new Vector2(input.movement * movementSpeed * Time.deltaTime, 0));

            if (input.Jump() && groundCheck.isGrounded) rb.AddForce(new Vector2(0, jumpPower));
        }
    }
}
