using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy[] enemies;

        [SerializeField] private float timeBetweenEnemySpawn = 2.5f;

        private float timeSinceLastEnemySpawn = 2f;

        private void Update()
        {
            if(timeSinceLastEnemySpawn >= timeBetweenEnemySpawn)
            {
                var spawnedEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector2(0f, transform.position.y), default, null);
                spawnedEnemy.Load();

                timeSinceLastEnemySpawn = 0f;
            }

            timeSinceLastEnemySpawn += Time.deltaTime;
        }
    }
}
