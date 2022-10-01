using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class Cube : GameEntity
    {
        private Collider2D col;
        private Rigidbody2D rb; 

        private void Awake()
        {
            // TODO: this is temp until loading implementing
            this.Load();
        }

        private void Start()
        {
            col = GetComponent<Collider2D>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var ray = Physics2D.Raycast(new Vector2(transform.position.x, col.bounds.min.y - 0.05f), Vector2.down, 0.05f);

            if(!ray)
            {
                rb.velocity = Vector2.down * 10 * Time.deltaTime;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector2 direction = Vector2.down * 1f;
            Gizmos.DrawRay(transform.position, direction);
        }
    }
}
