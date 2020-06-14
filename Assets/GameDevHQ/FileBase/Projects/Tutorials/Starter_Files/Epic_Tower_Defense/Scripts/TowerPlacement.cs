using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private GameObject[] _decoyTowers;
    [SerializeField] private GameObject[] _towers;

    public static event Action placeTower;
    public static event Action towerPlaced;

    private int _towerID;
    private bool _canPlaceTower = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _towerID = 0;
            _decoyTowers[1].gameObject.SetActive(false);
            _decoyTowers[_towerID].gameObject.SetActive(true);

            _canPlaceTower = true;

            if (placeTower != null)
            {
                placeTower();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _towerID = 1;
            _decoyTowers[0].gameObject.SetActive(false);
            _decoyTowers[_towerID].gameObject.SetActive(true);

            _canPlaceTower = true;

            if (placeTower != null)
            {
                placeTower();
            }     
        }

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

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            _decoyTowers[_towerID].transform.position = hitInfo.point;
        }    
    }

    public void PlaceTower(LocationScript pos)
    {
        {
            ITower obj = _towers[_towerID].GetComponent<ITower>();

            bool check = CurrencyManager.Instance.HaveFunds(obj.WarFundsRequired);

            if ((check == true) && (_canPlaceTower == true))
            {
                Instantiate(_towers[_towerID], pos.gameObject.transform.position, Quaternion.identity);
                _canPlaceTower = false;

                _decoyTowers[_towerID].gameObject.SetActive(false);
            }

            else
            {
                Debug.Log("Not enough warfunds");
                return;
            }

            if (obj != null)
            {
                CurrencyManager.Instance.PayTower(obj.WarFundsRequired);
            }

            if (towerPlaced != null)
            {
                towerPlaced();
            }       
        }
    }

    public void SnapTower(Vector3 spot)
    {
        _decoyTowers[_towerID].gameObject.transform.position = spot;
    }
}
