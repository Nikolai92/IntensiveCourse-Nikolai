﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _decoyTowers;
    [SerializeField] private GameObject[] _towers;
    [SerializeField] private GameObject _towerContainer;
    
    private int _towerID; // 1 = gatling, 2 = turret
    private bool _canPlaceTower = false;
    private bool _isSnap = false;

    public static event Action placeTower;
    public static event Action towerPlaced;

    private ITower currentTower;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseCtrl();    
    }

    private void MouseCtrl()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Input.GetMouseButtonDown(1))
        {
            _decoyTowers[_towerID].gameObject.SetActive(false);
            _canPlaceTower = false;

            if (towerPlaced != null)
            {
                towerPlaced();
            }
            return;
        }

        if (Physics.Raycast(rayOrigin, out hitInfo) && _isSnap == false)
        {
            _decoyTowers[_towerID].transform.position = hitInfo.point;
        }    
    }

    public ITower PlaceTower(Vector3 pos, ITower currentTower)
    {
        {
            ITower obj = _towers[_towerID].GetComponent<ITower>();

            bool check = CurrencyManager.Instance.HaveFunds(obj.WarFundsRequired);

            if ((check == true) && (_canPlaceTower == true))
            {
                GameObject initialTower = Instantiate(_towers[_towerID], pos, Quaternion.identity);
                initialTower.transform.SetParent(_towerContainer.transform, true);
                currentTower = initialTower.GetComponent<ITower>();
                currentTower.CurrentTowerObject = initialTower;
                currentTower.PlacedTowerPos = pos;
                _canPlaceTower = false;

                _decoyTowers[_towerID].gameObject.SetActive(false);

                return currentTower;
            }

            if (obj != null)
            {
                CurrencyManager.Instance.PayTower(obj.WarFundsRequired);
            }

            if (towerPlaced != null)
            {
                towerPlaced();
            }

            return null;
        }
    }

    public void SnapTower(Vector3 spot)
    {
        _isSnap = true;
        _decoyTowers[_towerID].gameObject.transform.position = spot;
    }

    public void UnSnapTower()
    {
        _isSnap = false;
    }

    public void SelectTower(int towerIndex)
    {
        _towerID = towerIndex;
        _decoyTowers[_towerID].gameObject.SetActive(false);
        _decoyTowers[_towerID].gameObject.SetActive(true);

        _canPlaceTower = true;

        if (placeTower != null)
        {
            placeTower();
        }
    }

    public void UpgradeTower(Vector3 pos, ITower currentTower)
    {
        Destroy(currentTower.CurrentTowerObject);
        GameObject upgrade = Instantiate(currentTower.UpgradedTowerObject);

        currentTower = upgrade.GetComponent<ITower>();
        upgrade.transform.position = pos;
        currentTower.PlacedTowerPos = pos;
        currentTower.CurrentTowerObject = upgrade;
    }
}
