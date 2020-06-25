using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : AI
{
    [SerializeField] public float _health = 0f;
    private float _originalHealth;
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
        _originalHealth = _health;
      
    }

    public void Update()
    {
        
    }

    private IEnumerator DieAndDespawn()
    {
        _explosion.Play();
        yield return new WaitForSeconds(_timeToDespawn);
        this.gameObject.SetActive(false);
        _health = _originalHealth;
        isDead = false;
        animator.ResetTrigger("IsDead");
    }

    public float GatlingGunAttack(int damage, float dps)
    {
        _health -= (damage *  dps);

        if (_health <= 0)
        {
            animator.SetTrigger("IsDead");
            isDead = true;
            StopWalking();
            StartCoroutine(DieAndDespawn());
        }

        return _health;     
    }

   /* public float TurretAttack(int damage)
    {
        StartCoroutine(FireRocketsRoutine)
    }*/
}
