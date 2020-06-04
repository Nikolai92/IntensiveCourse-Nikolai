using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] private Transform spawnStart;
    [SerializeField] private Transform spawnEnd;

    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private int _currentWave;
    [SerializeField] private int _numberOfEnemies;
    [SerializeField] private int _waveMultiplier = 10;

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaveSpawner());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var enemy = PoolManager.Instance.RequestEnemy();
        }
    }

    private IEnumerator WaveSpawner()
    {
        while (_numberOfEnemies < AmountToSpawn())
        {
            var enemy = PoolManager.Instance.RequestEnemy();
            yield return new WaitForSeconds(_timeBetweenSpawn);
            _numberOfEnemies++;
        }
    }

    private int AmountToSpawn()
    {
        var amountToSpawn = _waveMultiplier * _currentWave;
        return amountToSpawn;
    }

    public override void Init()
    {
        base.Init();
        Debug.Log("Wavespawn has started");
    }

    public Vector3 RequestTarget()
    {
        return spawnEnd.transform.position;
    }

    public Vector3 RequestStartPos()
    {
        return spawnStart.transform.position;
    }



}
