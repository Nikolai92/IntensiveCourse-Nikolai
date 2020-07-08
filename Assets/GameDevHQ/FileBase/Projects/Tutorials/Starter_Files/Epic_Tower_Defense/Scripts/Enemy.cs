using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Enemy : AI
{    
    [SerializeField] private int _warFund;
    [SerializeField] private float _timeToDespawn = 3f;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private List<Renderer> _diffuse;
    [SerializeField] public float _health = 0f;
    [SerializeField] public Animator animator;

    private float _originalHealth;
    private float _difusseSpeed;
    public bool isDead = false;

    private WaitForSeconds _deathDelayYield;

    public static event Action<int> HasDiedGetFunds;

    private IEnumerator coroutine;

    public override void Start()
    {
        base.Start();
        _explosion.Stop();
        _originalHealth = _health;
        _diffuse = GetComponentsInChildren<Renderer>().ToList();
        _deathDelayYield = new WaitForSeconds(_timeToDespawn);
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
            fill += (Time.deltaTime / 4);

            _diffuse.ForEach(rend => rend.material.SetFloat("_Amount", fill));

            yield return new WaitForEndOfFrame();
        }

        yield return _deathDelayYield;

        animator.ResetTrigger("IsDead");
        animator.WriteDefaultValues();
        this.gameObject.SetActive(false);
        _health = _originalHealth;
        isDead = false;
        _explosion.Stop();
    }

    public float BeingAttacked(int damage, float dps)
    {
        _health -= (damage *  dps);

        if (_health <= 0 && isDead == false)
        {
            if (HasDiedGetFunds != null)
            {
                HasDiedGetFunds(_warFund);
            }
            animator.SetTrigger("IsDead");
            
            isDead = true;
            StopWalking();

            StartCoroutine(DieAndDespawn());      
            
        }
        return _health;     
    }
}
