using System;
using System.Collections;
using CoreBreach.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace CoreBreach.Waves
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemyPrefabs;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Transform coreTarget;
        [SerializeField] private int[] enemiesPerWave ={ 3, 5, 7 };
        [SerializeField] private float spawnDelay =1.5f;
        [SerializeField] private float waveDelay =4f;
        [SerializeField] private float spawnRadius =1.2f;

        public event Action AllWavesCleared;

        private int currentWave;
        private int aliveEnemies;
        private bool allWavesFinished;

        private void Start()
        {
            StartCoroutine(SpawnWaves());
        }

        private IEnumerator SpawnWaves()
        {
            while (currentWave < enemiesPerWave.Length)
            {
                yield return StartCoroutine(SpawnSingleWave(enemiesPerWave[currentWave]));

                //wait until current wave enemies are destroyed
                yield return new WaitUntil(() => aliveEnemies <= 0);

                currentWave++;

                yield return new WaitForSeconds(waveDelay);
            }

            if (!allWavesFinished)
            {
                allWavesFinished=true;
                AllWavesCleared?.Invoke();
            }
        }

        private IEnumerator SpawnSingleWave(int enemyCount)
        {
            for (int i =0; i < enemyCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        private void SpawnEnemy()
        {
            if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0)
            {
                return;
            }

            GameObject prefab = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
            Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

            Vector3 randomOffset =UnityEngine.Random.insideUnitSphere * spawnRadius;
            randomOffset.y =0f;

            Vector3 wantedPosition = spawnPoint.position + randomOffset;

            //spawn only on the navmesh
            if (!NavMesh.SamplePosition(wantedPosition, out NavMeshHit hit, 3f,NavMesh.AllAreas))
            {
                return;
            }

            GameObject enemy = Instantiate(prefab,hit.position,spawnPoint.rotation);
            aliveEnemies++;

            //send enemy to the core
            EnemyMover mover = enemy.GetComponent<EnemyMover>();
            if (mover != null)
            {
                mover.SetTarget(coreTarget);
            }

            EnemyCoreAttacker attacker = enemy.GetComponent<EnemyCoreAttacker>();
            if (attacker != null)
            {
                attacker.SetTarget(coreTarget);
            }

            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.Died += HandleEnemyDied;
            }

        }

        private void HandleEnemyDied(EnemyHealth enemyHealth)
        {
            enemyHealth.Died -= HandleEnemyDied;
            aliveEnemies--;
        }
    }
}