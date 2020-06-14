using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AI : MonoBehaviour
{
    [SerializeField] protected Vector3 _target;

    protected NavMeshAgent _agent; 

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    public void OnEnable()
    {
        _target = SpawnManager.Instance.RequestTarget();

        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null)
        {
            _agent.SetDestination(_target);
        }
    }

    public void OnDisable()
    {
        this.transform.position = SpawnManager.Instance.RequestStartPos();
    }
}
