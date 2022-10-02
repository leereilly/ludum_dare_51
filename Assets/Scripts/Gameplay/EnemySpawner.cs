using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Helzinko
{
    [Serializable]
    public class LevelPool
    {
        public int maxEnemiesOnScreen;
        public float flyingEnemyShootingSpeed;
        public float enemySpawningSpeed;
        public float bomberSpeed;
        public Enemy[] spawningEnemies;
    }

    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner instance;

        public LevelPool[] levelPools;

        private void Awake()
        {
            instance = this;
        }

        private List<Enemy> spawnedEnemies = new List<Enemy>();

        [SerializeField] private float timeBetweenEnemySpawn = 2.5f;

        private float timeSinceLastEnemySpawn = 2f;

        public void RemoveEnemy(Enemy enemy)
        {
            spawnedEnemies.Remove(enemy);
        }

        private void Update()
        {
            if(GetCurrentPool().maxEnemiesOnScreen > spawnedEnemies.Count)
            {
                if (timeSinceLastEnemySpawn >= GetCurrentPool().enemySpawningSpeed)
                {
                    var spawnedEnemy = Instantiate(GetCurrentPool().spawningEnemies[UnityEngine.Random.Range(0, GetCurrentPool().spawningEnemies.Length)], new Vector2(UnityEngine.Random.Range(-3.5f, 3.5f), transform.position.y), default, null);
                    spawnedEnemy.Load();
                    spawnedEnemies.Add(spawnedEnemy);

                    timeSinceLastEnemySpawn = 0f;
                }
            }

            timeSinceLastEnemySpawn += Time.deltaTime;
        }

        public LevelPool GetCurrentPool()
        {
            var id = Gameplay.instance.gameplayLevel >= levelPools.Length ? levelPools.Length - 1 : Gameplay.instance.gameplayLevel;
            return levelPools[id];
        }
    }
}
