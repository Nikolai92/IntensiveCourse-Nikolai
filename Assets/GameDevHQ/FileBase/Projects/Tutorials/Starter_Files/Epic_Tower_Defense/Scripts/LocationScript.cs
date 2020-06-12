using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private GameObject _greenCircle;
    [SerializeField] private GameObject _redCircle;


    public void Start()
    {
        DeactivateParticles();
    }

    private void OnEnable()
    {
        TowerPlacement.placeTower += ActivateParticles;
        TowerPlacement.placeTower += ShowPositionsON;
        TowerPlacement.placeTower += ShowPositionsOFF;
        TowerPlacement.towerPlaced += DeactivateParticles;
    }

    private void OnDisable()
    {
        TowerPlacement.placeTower -= ActivateParticles;
        TowerPlacement.placeTower -= ShowPositionsON;
        TowerPlacement.placeTower -= ShowPositionsOFF;
        TowerPlacement.towerPlaced -= DeactivateParticles;
    }

    public void OnMouseEnter()
    {
        ShowPositionsON();
    }

    

    public void OnMouseDown()
    {
        //Check if spot is available
    }

    public void OnMouseExit()
    {
        ShowPositionsOFF();
    }

    

    public void ActivateParticles()
    {
        _particles.Play();
    }

    public void DeactivateParticles()
    {
        _particles.Stop();
    }

    private void ShowPositionsON()
    {
        _greenCircle.SetActive(true);
        _redCircle.SetActive(false);
    }

    private void ShowPositionsOFF()
    {
        _greenCircle.SetActive(false);
        _redCircle.SetActive(true);
    }


}
