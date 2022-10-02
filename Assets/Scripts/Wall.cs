using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class Wall : MonoBehaviour
    {
        public GameObject bottomWall = null;
        public Wall wallPrefab;

        private bool playerPassed = false;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (playerPassed) return;

            if(collision.TryGetComponent(out Player player))
            {
                if (bottomWall) Destroy(gameObject);

                var wall = Instantiate(wallPrefab, new Vector2(transform.position.x, transform.position.y + 27.95f), default, null);
                wall.bottomWall = gameObject;

                playerPassed = true;
            }
        }
    }
}
