using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : AI
{
   [SerializeField] private int _health = 0;

   [SerializeField] private int _warFund = 0;

    public override void Start()
    {
        base.Start();

    }
}
