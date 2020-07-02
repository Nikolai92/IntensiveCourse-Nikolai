using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private Transform _mech;
    [SerializeField] private float _speed = 4;
    [SerializeField] private Transform startingPos;
    [SerializeField] private GameObject _objectToRotate;
    [SerializeField] private int _damage = 10;
    [SerializeField] private int _perSecond = 1;
    [SerializeField] public List<GameObject> enemyList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        startingPos = _objectToRotate.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        { 
            enemyList.Add(other.gameObject);
            
            if (enemyList.Count > 0)
            {
                AimTarget(enemyList[0].transform);
            }
            else
            {
                AimTarget(startingPos);
            }
        }    
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (enemyList.Count > 0)
        {
            AimTarget(enemyList[0].transform);
            enemyList[0].GetComponent<Enemy>().BeingAttacked(_damage, Time.deltaTime);

            if (enemyList[0].GetComponent<Enemy>().isDead == true)
            {
                enemyList.Remove(other.gameObject);

                if (enemyList.Count > 0)
                {
                    AimTarget(enemyList[0].transform);
                }

                else
                {
                    AimTarget(startingPos);
                }
            }   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyList.Remove(other.gameObject);     
        }

        else if (enemyList.Count > 0)
        {
            AimTarget(enemyList[0].transform);
        }

        else
        {
            AimTarget(startingPos);
        }
    }

    private void AimTarget(Transform target)
    {
        Vector3 targetDirection = target.transform.position - _objectToRotate.transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        _objectToRotate.transform.rotation = Quaternion.Slerp(_objectToRotate.transform.rotation, targetRotation, Time.deltaTime * _speed);
    }
}
