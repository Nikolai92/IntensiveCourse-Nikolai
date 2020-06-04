using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] private Transform _spawnStart;
    [SerializeField] public Transform spawnEnd;

    [SerializeField] private GameObject[] _enemies;

    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private int _currentWave;
    [SerializeField] private int _numberOfEnemies;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(WaveSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaveSpawner()
    {
        while (_numberOfEnemies < AmountToSpawn())
        {

            var enemy = GetComponent<PoolManager>().RequestEnemy();
            //var enemy = Instantiate(_enemies[RandomEnemy()], _spawnStart);
            enemy.GetComponent<AI>()._target = spawnEnd;
            yield return new WaitForSeconds(_timeBetweenSpawn);
            _numberOfEnemies++;
        }
    }

    private int RandomEnemy()
    {
        var randomNumber = Random.Range(0, _enemies.Length);
        return randomNumber;
    }

    private int AmountToSpawn()
    {
        var amountToSpawn = 10 * _currentWave;
        return amountToSpawn;
    }

    public override void Init()
    {
        base.Init();
        Debug.Log("Wavespawn has started");
    }



}
