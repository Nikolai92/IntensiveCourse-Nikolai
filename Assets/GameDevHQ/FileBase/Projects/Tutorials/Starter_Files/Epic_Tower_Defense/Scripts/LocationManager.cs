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
    [SerializeField] private UIManager _UIManager;

    private bool _towerHasBeenPlaced = false;
    private bool _hasBeenUpgraded = false;

    private ITower _currentTower;
    private IEnumerator coroutine;

    public static event Action<ITower> TowerMenu;

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
            _currentTower = _tower.PlaceTower(this.transform.position, _currentTower);
            _towerHasBeenPlaced = true;
            return;
        }

        else if (_towerHasBeenPlaced == true)
        {
            if (TowerMenu != null)
            {
                TowerMenu(_currentTower);
                Debug.Log("Tower is: " + _currentTower.TowerID);
                //Fire off event with ITower selected
            }


            /* if (UpgradeTower != null)
             {
                 UpgradeTower(this.transform.position, _currentTower);
             } */
            /*if (_hasBeenUpgraded == false)
            {
                _UIManager.TowerMenu(_currentTower.TowerID);
                _UIManager.SellUIMenu();
            }

            else if (_hasBeenUpgraded == true)
            {
                _UIManager.SellUIMenu();
            }*/
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

   

    /*public void TowerSell()
    {
        _tower.SellTower(_currentTower);
        CurrencyManager.Instance.TowerSell(_currentTower.SellRefund);
        _towerHasBeenPlaced = false;
    }*/
}
