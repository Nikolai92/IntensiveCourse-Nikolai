using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private GameObject[] _upgradeUI;
    [SerializeField] private GameObject _sellUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TowerMenu(int towerID)
    {
        _upgradeUI[towerID].SetActive(true);
    }

    public void DisableUpgradeMenu(int towerID)
    {
        _upgradeUI[towerID].SetActive(false);      
    }
    public void SellUIMenu()
    {
       _sellUI.SetActive(true);
    }

    public void DisableSellMenu()
    {
       _sellUI.SetActive(false);
    }
}
