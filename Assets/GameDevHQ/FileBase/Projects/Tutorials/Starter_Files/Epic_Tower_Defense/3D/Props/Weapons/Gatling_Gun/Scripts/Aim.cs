using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{

    [SerializeField] private Transform _mech;
    [SerializeField] private float _speed = 4;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = _mech.position - transform.position;

        //transform.rotation = Quaternion.LookRotation(targetDirection);

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        _mech = other.gameObject.transform;
        Debug.Log("Turret colliding with: " + other);
    }
}
