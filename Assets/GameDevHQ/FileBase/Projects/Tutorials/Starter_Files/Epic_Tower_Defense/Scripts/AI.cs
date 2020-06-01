using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : Enemy
{

    [SerializeField] private GameObject _target;

    private NavMeshAgent _agent; 

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
