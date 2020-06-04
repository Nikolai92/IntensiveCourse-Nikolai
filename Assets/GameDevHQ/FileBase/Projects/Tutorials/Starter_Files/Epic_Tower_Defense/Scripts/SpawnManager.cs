using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] public Transform spawnStart;
    [SerializeField] public Transform spawnEnd;

    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private int _currentWave;
    [SerializeField] private int _numberOfEnemies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaveSpawner());
    }

    private IEnumerator WaveSpawner()
    {
        while (_numberOfEnemies < AmountToSpawn())
        {
            var enemy = PoolManager.Instance.RequestEnemy();
            enemy.GetComponent<AI>()._target = spawnEnd;
            yield return new WaitForSeconds(_timeBetweenSpawn);
            _numberOfEnemies++;
        }
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
