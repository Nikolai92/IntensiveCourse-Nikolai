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
        _agent = GetComponent<NavMeshAgent>();
    }
    
    public void StopWalking()
    {
        if (_agent != null)
        {
            _agent.isStopped = true;
        }
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
        //this.GetComponent<Animator>().SetBool("IsDead", false);
        this.GetComponent<Animator>().ResetTrigger("IsDead");
    }
}
