using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private GameObject[] _decoyTowers;
    [SerializeField] private GameObject[] _towers;

    public static Action placeTower;
    public static Action towerPlaced;


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

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            _decoyTowers[0].transform.position = hitInfo.point;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (placeTower != null)
                {
                    placeTower();
                }
            }

            if (Input.GetMouseButtonDown(0) && (hitInfo.collider.tag == "ValidSpot"))
            {
                Instantiate(_decoyTowers[0], hitInfo.collider.transform.position, Quaternion.identity);
                towerPlaced();
            }

        }
    }
}
