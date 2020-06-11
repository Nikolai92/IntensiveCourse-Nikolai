using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    
    public void Start()
    {
        DeactivateParticles();
    }

    private void OnEnable()
    {
        TowerPlacement.placeTower += ActivateParticles;
        TowerPlacement.towerPlaced += DeactivateParticles;
    }

    private void OnDisable()
    {
        TowerPlacement.placeTower -= ActivateParticles;
        TowerPlacement.towerPlaced -= DeactivateParticles;
    }

    public void OnMouseEnter()
    {
        //Go green
    }

    public void OnMouseDown()
    {
        //Check if spot is available
    }

    public void OnMouseExit()
    {
        //Go red
    }

    public void ActivateParticles()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }

    public void DeactivateParticles()
    {
        GetComponentInChildren<ParticleSystem>().Stop();
    }
}
