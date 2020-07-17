using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] private Transform spawnStart;
    [SerializeField] private Transform spawnEnd;

    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private int _currentWave;
    [SerializeField] private int _numberOfEnemies;
    [SerializeField] private int _waveMultiplier = 10;

    [SerializeField] private Text mainScreen;
    [SerializeField] private Text waveNumber;
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    private int _waveNumber;

    public static event Action Victory;

    // Start is called before the first frame update
    void Start()
    {
        waveNumber.text = _waveNumber.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (_waveNumber <= 10))
        {
            StartCoroutine(WaveSpawner());
            _waveNumber++;
            waveNumber.text = _waveNumber.ToString();

            if(_waveNumber == 10)
            {
                mainScreen.text = "LEVEL \nCOMPLETE";

                if (Victory != null)
                {
                    Victory();
                }
            }
        }
    }

    private IEnumerator WaveSpawner()
    {
        //_currentWave = 0;

        while (_currentWave < _waves.Count)
        {
            //_currentWave++;

            /*var currentWave = _waves[_currentWave].sequence;
            for (int i = 0; i > currentWave.Count; i++)
            {
                Instantiate(currentWave[i], spawnStart.transform);
                yield return new WaitForSeconds(1f);
            }

            if (_currentWave == _waves.Count)
            {
                Debug.Log("Waves finished");
            }*/

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
