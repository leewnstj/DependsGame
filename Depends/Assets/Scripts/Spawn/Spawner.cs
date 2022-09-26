using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnModes
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    [Header("¼¼ÆÃ")]
    [SerializeField] private SpawnModes spawnModes = SpawnModes.Fixed;

    [SerializeField] private int enemyCount = 10;

    [Header("Fixed delay")]
    [SerializeField] private float delayBtwSpawns;

    [Header("Random delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    private ObjectPooler _pooler;
    private WayPoint _waypoint;

    private float _spawnTimer;
    private float _enemiesSpawned;
    private int _enemiesRemainning;

    void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        _waypoint = GetComponent<WayPoint>();

        _enemiesRemainning = enemyCount;
    }

    void Update()
    {
        if (spawnModes == SpawnModes.Fixed)
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer < 0)
            {
                _spawnTimer = delayBtwSpawns;
                if (_enemiesSpawned < enemyCount)
                {
                    _enemiesSpawned++;
                    SpawnEnemy();
                }
            }
        }
        else
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer < 0)
            {
                _spawnTimer = GetRandomDelay();
                if (_enemiesSpawned < enemyCount)
                {
                    _enemiesSpawned++;
                    SpawnEnemy();
                }
            }
        }
    }

    private void SpawnEnemy()
    { 
        GameObject newInstatance = _pooler.GetInstanceFromPool();
        Enemy enemy = newInstatance.GetComponent<Enemy>();
        enemy.wayPoint = _waypoint;

        enemy.transform.localPosition = transform.position;
        enemy.ResetEnemy();
        newInstatance.SetActive(true);
    }

    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }

    private void RecordEnemy()
    {
        _enemiesRemainning--;
        if(_enemiesRemainning <= 0)
        {
            StartCoroutine(NextWave());
        }
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwSpawns);
        _enemiesRemainning = enemyCount;
        _spawnTimer = 0;
        _enemiesSpawned = 0;
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;
    }
}
