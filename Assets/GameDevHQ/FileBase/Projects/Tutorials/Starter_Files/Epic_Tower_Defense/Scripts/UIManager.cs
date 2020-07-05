using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private GameObject[] _upgradeUI;
    [SerializeField] private GameObject _sellUI;

    [SerializeField] private Text Lives;


    [SerializeField] private int _lives;


    ITower _currentTower;

    private void OnEnable()
    {
        LocationManager.TowerMenu += TowerMenu;
        LocationManager.TowerMenu += SellUIMenu;
        EndRecycler.EnemyReachedEnd += TakeLife;

    }

    private void OnDisable()
    {
        LocationManager.TowerMenu -= TowerMenu;
        LocationManager.TowerMenu -= SellUIMenu;
        EndRecycler.EnemyReachedEnd -= TakeLife;
    }

    // Start is called before the first frame update
    void Start()
    {
        Lives.text = _lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TowerMenu(ITower tower)
    {
        _currentTower = tower;
        _upgradeUI[tower.TowerID].SetActive(true);    
    }

    public void DisableTowerMenu()
    {
        _upgradeUI[_currentTower.TowerID].SetActive(false);      
    }

    public void SellUIMenu(ITower tower)
    {
       _sellUI.SetActive(true);
    }

    public void DisableSellMenu()
    {
       _sellUI.SetActive(false);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void TakeLife()
    {
        _lives--;
        Lives.text = _lives.ToString();
    }

    public void FastForward()
    {
        Time.timeScale = 2f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
    }
}
