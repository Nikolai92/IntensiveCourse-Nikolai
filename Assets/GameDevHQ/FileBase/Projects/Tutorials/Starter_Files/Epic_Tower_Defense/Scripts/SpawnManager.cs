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
    private int _currentWave;
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
        if (Input.GetKeyDown(KeyCode.Space) && (_waveNumber <= 3))
        {
            StartCoroutine(WaveSpawner());
            
            if(_waveNumber == 3)
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
        while(true)
        {
            _waveNumber++;
            waveNumber.text = _waveNumber.ToString();
            
            var currentWave = _waves[_currentWave].sequence;
            //var previousWave = new GameObject("PreviousWave");
            foreach (var obj in currentWave)
            {
                Instantiate(obj, spawnStart);
                yield return new WaitForSeconds(1.0f);
            }
            
            yield return new WaitForSeconds(5.0f);


            //Destroy(previousWave);
            _currentWave++;

            if (_currentWave == _waves.Count)
            {
                Debug.Log("Waves finished");
                break;
            }

        }  
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
