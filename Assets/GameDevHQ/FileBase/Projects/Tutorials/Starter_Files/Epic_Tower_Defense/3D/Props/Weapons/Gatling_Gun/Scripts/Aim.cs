using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{

    [SerializeField] private Transform _mech;
    [SerializeField] private float _speed = 4;
    private Transform startingPos;

    [SerializeField] private GameObject _objectToRotate;

    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();

    public static event Action mechHasEntered;
    public static event Action mechHasExited;

    private IEnumerator coroutine;


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
            _enemyList.Add(other.gameObject);
            
            AimTarget(_enemyList[0].transform);
            

            if (mechHasEntered != null)
            {
                mechHasEntered();
            }
        }      
    }
    private void OnTriggerStay(Collider other)
    {
        AimTarget(_enemyList[0].transform);
    }

    private void OnTriggerExit(Collider other)
    {
        _enemyList.Remove(other.gameObject);

        if (_enemyList == null)
        {
            AimTarget(startingPos);
        }

        else if (_enemyList != null)
        {
            AimTarget(_enemyList[0].transform);
        }
        

        if (mechHasExited != null)
        {
            mechHasExited();
        }
    }

    private void AimTarget(Transform target)
    {
        Vector3 targetDirection = _enemyList[0].transform.position - _objectToRotate.transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        _objectToRotate.transform.rotation = Quaternion.Slerp(_objectToRotate.transform.rotation, targetRotation, Time.deltaTime * _speed);
    }

    /*private void AimTarget(Transform target)
    {
        Vector3 targetDirection = _mech.position - _objectToRotate.transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        _objectToRotate.transform.rotation = Quaternion.Slerp(_objectToRotate.transform.rotation, targetRotation, Time.deltaTime * _speed);
    }*/
}
