using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _enemyPrefab;
    [SerializeField] private List<GameObject> _enemyPool;


    [SerializeField] private int numberOfEnemies = 10;

    private void Start()
    {
        _enemyPool = GenerateEnemies(numberOfEnemies);
    }

    List<GameObject> GenerateEnemies(int amountOfEnemies)
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab[RandomEnemy()], StartPos(), Quaternion.identity);
            enemy.transform.SetParent(_enemyContainer.transform, false);
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

        GameObject newEnemy = Instantiate(_enemyPrefab[RandomEnemy()], StartPos(), Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
        _enemyPool.Add(newEnemy);

        return newEnemy;
    }

    private int RandomEnemy()
    {
        var randomNumber = Random.Range(0, _enemyPrefab.Length);
        return randomNumber;
    }

    private Vector3 StartPos() 
    {
        var pos = SpawnManager.Instance.RequestStartPos();
        return pos;
    } 
}
