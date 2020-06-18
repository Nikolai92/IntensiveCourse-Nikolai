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

    [SerializeField] private Queue _enemyQueue = new Queue();

    public static event Action mechHasEntered;
    public static event Action mechHasExited;


    // Start is called before the first frame update
    void Start()
    {
        startingPos = _objectToRotate.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = _mech.position - _objectToRotate.transform.position;

        //transform.rotation = Quaternion.LookRotation(targetDirection);

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        _objectToRotate.transform.rotation = Quaternion.Slerp(_objectToRotate.transform.rotation, targetRotation, Time.deltaTime * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _enemyQueue.Enqueue(other.gameObject);
            _mech = other.gameObject.transform;
            mechHasEntered();
            Debug.Log("Turret colliding with: " + other);
        }      
    }
    private void OnTriggerStay(Collider other)
    {
        //_enemyQueue.Contains.other = other.gameObject.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        _enemyQueue.Dequeue();
        _mech = startingPos;
        mechHasExited();
    }
}
