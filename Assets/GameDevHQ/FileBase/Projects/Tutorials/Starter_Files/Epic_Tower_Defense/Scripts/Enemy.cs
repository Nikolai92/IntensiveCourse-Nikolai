using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : AI
{
    [SerializeField] public int _health = 0;
    [SerializeField] private int _warFund = 0;
    [SerializeField] private float _timeToDespawn = 3f;

    [SerializeField] public Animator animator;
    [SerializeField] private ParticleSystem _explosion;

    public bool isDead = false;

    private IEnumerator coroutine;

    public override void Start()
    {
        base.Start();
        _explosion.Pause();
      
    }

    public void Update()
    {
        
    }

    private IEnumerator DieAndDespawn()
    {
        while (true)
        {
            _explosion.Play();
            yield return new WaitForSeconds(_timeToDespawn);
            this.gameObject.SetActive(false);
        }
    }

    public int BeingAttacked(int damage, int dps)
    {
        _health -= damage *  dps;

        if (_health <= 0)
        {
            animator.SetBool("IsDead", true);
            isDead = true;
            StopWalking();
            
            StartCoroutine(DieAndDespawn());
        }

        return _health;     
    }
}
