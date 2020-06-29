using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private GameObject _greenCircle;
    [SerializeField] private GameObject _redCircle;

    [SerializeField] private ParticleSystem _particles;

    [SerializeField] private TowerManager _tower;

    private IEnumerator coroutine;

    private bool _towerHasBeenPlaced = false;

    public static event Action upgradeTower;

    private int _towerID;

    public void Start()
    {
        DeactivateParticles();
    }

    private void OnEnable()
    {
        TowerManager.placeTower += ActivateParticles;
        TowerManager.towerPlaced += DeactivateParticles;
    }

    private void OnDisable()
    {
        TowerManager.placeTower -= ActivateParticles;
        TowerManager.towerPlaced -= DeactivateParticles;
    }

    public void OnMouseEnter()
    {
        _tower.SnapTower(this.gameObject.transform.position);

        if (_towerHasBeenPlaced == true)
        {
            _redCircle.gameObject.SetActive(true);
        }

        else
        {
            _greenCircle.gameObject.SetActive(true);
            _redCircle.gameObject.SetActive(false);
        }
    }

    public void OnMouseDown()
    {
        if (_towerHasBeenPlaced == false)
        {
            _towerID = _tower.GetTowerID();
            _tower.PlaceTower(this);
            _towerHasBeenPlaced = true;
        }

        else if (_towerHasBeenPlaced == true)
        {
            //TowerManager.UpgradeTower(_towerID);
        }
    }

    public void OnMouseExit()
    {
        _tower.UnSnapTower();
        _greenCircle.gameObject.SetActive(false);
        _redCircle.gameObject.SetActive(true);
        coroutine = WaitAndDisable();
        StartCoroutine(coroutine);
    }

    public void ActivateParticles()
    {
        _particles.Play();
    }

    public void DeactivateParticles()
    {
        _particles.Stop();
    }

    IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(1.5f);
        _redCircle.gameObject.SetActive(false);
    }
}
