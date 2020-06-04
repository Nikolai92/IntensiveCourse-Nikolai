using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _enemyPrefab;
    [SerializeField] private List<GameObject> _enemyPool;


    [SerializeField] private int numberOfEnemies = 10;

    // Start is called before the first frame update
    void Start()
    {
        _enemyPool = GenerateEnemies(numberOfEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<GameObject> GenerateEnemies(int amountOfEnemies)
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab[Random.Range(0,_enemyPrefab.Length)]);
            enemy.transform.parent = _enemyContainer.transform;
            enemy.SetActive(false);

            _enemyPool.Add(enemy);
        }

        return _enemyPool;
    }

    public GameObject RequestEnemy()
    {
        foreach (var enemy in _enemyPool)
        {
            if (enemy.activeInHierarchy == false)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }

        GameObject newEnemy = Instantiate(_enemyPrefab[Random.Range(0, _enemyPrefab.Length)]);
        newEnemy.transform.parent = _enemyContainer.transform;
        _enemyPool.Add(newEnemy);

        return newEnemy;
    }
}
