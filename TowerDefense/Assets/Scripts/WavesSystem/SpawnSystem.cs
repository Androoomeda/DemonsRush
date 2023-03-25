using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SpawnSystem : MonoBehaviour
{
    [HideInInspector] public int aliveEnemies;

    [SerializeField] private Text timerText;
    [SerializeField] private float timeBtwWaves;

    private WaveSpawner[] spawners;
    private float timeLeft;

    public UnityEvent levelComplete;

    private void Start()
    {
        ServiceLocator.instance.Register(this);
        spawners = transform.GetComponentsInChildren<WaveSpawner>();

        timeLeft = timeBtwWaves;
    }

    private void Update()
    {
        if (timeLeft > 0)
            timeLeft -= Time.deltaTime;
        else if (transform.childCount != 0 && CheckWaveIsSpawned() && timeLeft <= 0)
            timeLeft = timeBtwWaves;

        TrySpawnNextWave();

        timerText.text = Mathf.Round(timeLeft).ToString();

        if (transform.childCount == 0 && aliveEnemies == 0)
            levelComplete.Invoke();
    }

    private void TrySpawnNextWave()
    {
        if (CheckWaveIsSpawned() && timeLeft <= 0 && transform.childCount != 0)
        {
            StartSpawners();
        }
    }

    private bool CheckWaveIsSpawned()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            if (!spawners[i].waveIsSpawned)
                return false;
        }
        return true;
    }

    private void StartSpawners()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].SpawnWave();
        }
    }
}
