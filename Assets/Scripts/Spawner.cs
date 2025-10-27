using UnityEngine;

public class Spawner : MonoBehaviour
{
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _player;
        [SerializeField] private PoisonSmoke _poisonSmoke;
        [SerializeField] private float _minimumSpawnTime;
        [SerializeField] private float _maximumSpawnTime;
        [SerializeField] private int _maxSpawns = 2;

        private float _timeUntilSpawn;
        private int _spawnCount = 0;

        void Awake()
        {
            SetTimeUntilSpawn();
        }

        void Update()
        {
            //if (_spawnCount >= _maxSpawns) return;
            _timeUntilSpawn -= Time.deltaTime;
            if (_timeUntilSpawn <= 0)
            {
                SpawnEnemy();
                //_spawnCount++;
                SetTimeUntilSpawn();
            }
        }

        private void SetTimeUntilSpawn()
        {
            _timeUntilSpawn = _maximumSpawnTime;
        }

        private void SpawnEnemy()
        {
            GameObject enemyGO = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            Enemy enemyScript = enemyGO.GetComponent<Enemy>();

            if (enemyScript != null)
            {
                enemyScript.Initialize(_poisonSmoke, _player);
            }
        }
        // it spawns but the enemies are being wack
        // i turned the max spawns code into // so it wouldnt be used as i didnt finsih in time
}
