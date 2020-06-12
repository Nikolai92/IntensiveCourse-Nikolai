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

            //turn on decoy (set active = true)
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

                if (Input.GetMouseButtonDown(0) && (hitInfo.collider.tag == "ValidSpot") && _canPlaceTower)
                {
                    Instantiate(_towers[_towerID], hitInfo.collider.transform.position, Quaternion.identity);
                    _canPlaceTower = false;

                    if (towerPlaced != null)
                    {
                        towerPlaced();
                    }                 
                }
            }    
    }
}
