using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public bool waveIsSpawned { get; private set; }

    [SerializeField] private Wave[] waves;

    private int waveIndex = 0;

    private void Awake()
    {
        waveIsSpawned = true;
    }

    public void SpawnWave()
    {
        waveIsSpawned = false;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int numberOfEnemyTypes = waves[waveIndex].enemyTypes.Length;

        for (int typeIndex = 0; typeIndex < numberOfEnemyTypes; typeIndex++)
        {
            int enemiesOfType = waves[waveIndex].enemyTypes[typeIndex].enemyCount;
            GameObject enemyPrefab = waves[waveIndex].enemyTypes[typeIndex].enemyPrefab;

            for (int i = 0; i < enemiesOfType; i++)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                newEnemy.GetComponent<EnemyMovement>().waypoints = waves[waveIndex].waypoints;

                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(1f);
        }

        if (waveIndex >= waves.Length - 1)
            Destroy(gameObject);

        waveIsSpawned = true;
        waveIndex++;
    }

    [System.Serializable]
    public class Wave 
    {
        public EnemyType[] enemyTypes;
        public Transform waypoints;
    }

    [System.Serializable]
    public class EnemyType
    {
        public GameObject enemyPrefab;
        public int enemyCount;
    }
}
