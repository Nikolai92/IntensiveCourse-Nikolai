using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private GameObject[] _upgradeUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeUIMenu(int towerID)
    {
        _upgradeUI[towerID].SetActive(true);
    }

    public void DisableUpgradeMenu(int towerID)
    {
        _upgradeUI[towerID].SetActive(false);
    }
}
