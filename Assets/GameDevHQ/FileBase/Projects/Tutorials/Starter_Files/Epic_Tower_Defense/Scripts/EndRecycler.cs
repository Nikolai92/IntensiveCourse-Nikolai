using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRecycler : MonoBehaviour
{
    public static event Action EnemyReachedEnd;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);

        if (EnemyReachedEnd != null)
        {
            EnemyReachedEnd();
        }      
    }
}
