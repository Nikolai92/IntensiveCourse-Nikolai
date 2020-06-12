using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private GameObject _greenCircle;
    [SerializeField] private GameObject _redCircle;

    private Color transparentColor = Color.red;

    public void Start()
    {
        transparentColor.a = 0f * Time.deltaTime;
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
        _greenCircle.gameObject.SetActive(true);
        _redCircle.gameObject.SetActive(false);
        
    }

    public void OnMouseDown()
    {

        //Check if spot is available
    }

    public void OnMouseExit()
    {
        _greenCircle.gameObject.SetActive(false);
        _redCircle.gameObject.SetActive(true);   
    }

    

    public void ActivateParticles()
    {
        _particles.Play();
    }

    public void DeactivateParticles()
    {
        _particles.Stop();
    }
}
