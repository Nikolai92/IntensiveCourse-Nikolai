using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScript : MonoBehaviour
{
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

    public void ActivateParticles()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }

    public void DeactivateParticles()
    {
        GetComponentInChildren<ParticleSystem>().Stop();
    }
}
