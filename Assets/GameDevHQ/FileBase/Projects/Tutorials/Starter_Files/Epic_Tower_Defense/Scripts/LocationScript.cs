using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScript : MonoBehaviour
{
    [SerializeField] private GameObject _greenCircle;
    [SerializeField] private GameObject _redCircle;

    [SerializeField] private ParticleSystem _particles;

    [SerializeField] private TowerPlacement tower;

    private Color transparentColor = Color.red;

    private bool _towerHasBeenPlaced = false;

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
        tower.SnapTower(this.gameObject.transform.position);      
    }

    public void OnMouseDown()
    {
        if (_towerHasBeenPlaced == false)
        {
            tower.PlaceTower(this);
            _towerHasBeenPlaced = true;
        }
        
        //TowerPlacement.PlaceTower(this);
        //Check if spot is available
    }

    public void OnMouseExit()
    {
        _greenCircle.gameObject.SetActive(false);
        _redCircle.gameObject.SetActive(true);
        //_redCircle.gameObject.GetComponent<Renderer>().material.color.a = 0f;
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
