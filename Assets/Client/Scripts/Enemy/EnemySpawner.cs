using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyBehavior> _enemies;
        [SerializeField] private Vector2 _xSpawnRange;
        [SerializeField] private Vector2 _ySpawnRange;
        [SerializeField] private int _enemyCount;

        private void Start()
        {
            GenerateEnemies();
        }

        private void GenerateEnemies()
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                float xPoint = Random.Range(_xSpawnRange.x, _xSpawnRange.y);
                float yPoint = Random.Range(_ySpawnRange.x, _ySpawnRange.y);

                EnemyBehavior enemy = _enemies[Random.Range(0, _enemies.Count)];

                Instantiate(enemy, new Vector3(xPoint, yPoint, 0f), Quaternion.identity);
            }
        }
    }
}