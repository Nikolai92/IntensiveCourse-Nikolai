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
            if (_hasBeenUpgraded == false)
            {
                _UIManager.TowerMenu(_currentTower.TowerID);
                _UIManager.SellUIMenu();
            }

            else if (_hasBeenUpgraded == true)
            {
                _UIManager.SellUIMenu();
            }
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

    public void TowerUpgrade()
    {
        bool check = CurrencyManager.Instance.HaveFunds(_currentTower.UpgradeCost);

        if (check == true)
        {
            CurrencyManager.Instance.TowerUpgradeCost(_currentTower.UpgradeCost);
            _tower.UpgradeTower(this.transform.position, _currentTower);
            _hasBeenUpgraded = true;
        }

        else
        {
            Debug.Log("Not enough funds");
        }
        
    }

    public void TowerSell()
    {
        _tower.SellTower(this.transform.position, _currentTower);
        CurrencyManager.Instance.TowerSell(_currentTower.SellRefund);
        _towerHasBeenPlaced = false;
    }
}
