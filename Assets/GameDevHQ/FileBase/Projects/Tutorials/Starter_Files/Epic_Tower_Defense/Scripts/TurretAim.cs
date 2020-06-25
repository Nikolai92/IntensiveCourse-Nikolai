using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour 
{

    [SerializeField] private Transform _mech;
    [SerializeField] private float _speed = 4;
    private Transform startingPos;

    [SerializeField] private GameObject _objectToRotate;

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
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (enemyList.Count > 0)
        {
            AimTarget(enemyList[0].transform);
            

            if (enemyList[0].GetComponent<Enemy>().isDead == true)
            {
                enemyList.Remove(other.gameObject);
                AimTarget(enemyList[0].transform);
            }

            else if (enemyList.Count == 0)
            {
                AimTarget(startingPos);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemyList.Remove(other.gameObject);

        if (enemyList.Count == 0)
        {
            AimTarget(startingPos);
        }

        else if (enemyList.Count > 0)
        {
            AimTarget(enemyList[0].transform);
        }

    }

    private void AimTarget(Transform target)
    {
        Vector3 targetDirection = enemyList[0].transform.position - _objectToRotate.transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        _objectToRotate.transform.rotation = Quaternion.Slerp(_objectToRotate.transform.rotation, targetRotation, Time.deltaTime * _speed);
    }
}