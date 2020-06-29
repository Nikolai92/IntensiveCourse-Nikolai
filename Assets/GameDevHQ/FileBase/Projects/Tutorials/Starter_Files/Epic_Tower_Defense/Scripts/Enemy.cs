using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Enemy : AI
{
    [SerializeField] public float _health = 0f;
    private float _originalHealth;
    [SerializeField] private int _warFund = 0;
    [SerializeField] private float _timeToDespawn = 3f;

    [SerializeField] public Animator animator;
    [SerializeField] private ParticleSystem _explosion;

    [SerializeField] private List<Renderer> _diffuse;

    private float _difusseSpeed;

    public bool isDead = false;

    private IEnumerator coroutine;

    public override void Start()
    {
        base.Start();
        _explosion.Stop();
        _originalHealth = _health;
        _diffuse = GetComponentsInChildren<Renderer>().ToList();


    }

    public void Update()
    {
        
    }

    private IEnumerator DieAndDespawn()
    {
        _explosion.Play();

        float fill = 0f;

        while (fill < 1f)
        {
            fill += (Time.deltaTime / 3);

            _diffuse.ForEach(rend => rend.material.SetFloat("_Amount", fill));

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(_timeToDespawn);

        animator.ResetTrigger("IsDead");
        animator.WriteDefaultValues();
        this.gameObject.SetActive(false);
        _health = _originalHealth;
        isDead = false;
        _explosion.Stop();
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
