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
            //turn on decoy (set active = true)
        }

        MouseCtrl();

        
    }

    private void MouseCtrl()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            _decoyTowers[_towerID].transform.position = hitInfo.point;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (placeTower != null)
                {
                    placeTower();
                }
            }

            /*if (Input.GetMouseButtonDown(0) && (hitInfo.collider.tag == "ValidSpot"))
            {
                Instantiate(_decoyTowers[0], hitInfo.collider.transform.position, Quaternion.identity);

                if (towerPlaced != null)
                {
                    towerPlaced();
                }
            }*/

        }
    }
}
