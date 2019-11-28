using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public enum SpawnState { Spawning,waiting,Counting};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    public int nextWave = 0;

    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    float searchCountDown = 1f;
    private SpawnState state = SpawnState.Counting;

    private void Start()
    {
        waveCountDown = timeBetweenWaves;
    }

    private void Update()
    {
        RoundScript.round = nextWave + 1;
        if(state == SpawnState.waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if(waveCountDown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }
    void WaveCompleted()
    {
        state = SpawnState.Counting;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }
    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
    
    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.Spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1 / _wave.rate);
        }

        state = SpawnState.waiting;
        yield break;
    }
    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0,spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
