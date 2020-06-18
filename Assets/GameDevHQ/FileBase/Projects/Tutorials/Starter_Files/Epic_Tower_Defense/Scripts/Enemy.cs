using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : AI
{
    [SerializeField] private int _health = 0;
    [SerializeField] private int _warFund = 0;
    [SerializeField] private float _timeToDespawn = 3f;

    [SerializeField] private Animator animator;

    private IEnumerator coroutine;

    public static event Action Died;

    public override void Start()
    {
        base.Start();
        
    }

    public void Update()
    {
        if (_health <= 0)
        {
            Died();
            animator.SetBool("IsDead", true);
            StartCoroutine(DieAndDespawn());

        }
    }

    private IEnumerator DieAndDespawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToDespawn);
            this.gameObject.SetActive(false);
        }
    }
}
