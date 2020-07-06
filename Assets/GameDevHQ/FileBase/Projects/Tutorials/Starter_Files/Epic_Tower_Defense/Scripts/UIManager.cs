using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private GameObject[] _upgradeUI;
    [SerializeField] private GameObject _sellUI;
    [SerializeField] private GameObject _statusScreen;
    [SerializeField] private Text Lives;
    [SerializeField] private int _lives;
    [SerializeField] private Text _statusScreenText;

    [SerializeField] private GameObject[] _blueUI;
    [SerializeField] private GameObject[] _yellowUI;
    [SerializeField] private GameObject[] _redUI;

    private bool gameHasStarted = false;

    IEnumerator coroutine;
    ITower _currentTower;

    private void OnEnable()
    {
        LocationManager.TowerMenu += TowerMenu;
        LocationManager.TowerMenu += SellUIMenu;
        EndRecycler.EnemyReachedEnd += TakeLife;
        SpawnManager.Victory += EnableStatusScreen;
    }

    private void OnDisable()
    {
        LocationManager.TowerMenu -= TowerMenu;
        LocationManager.TowerMenu -= SellUIMenu;
        EndRecycler.EnemyReachedEnd -= TakeLife;
        SpawnManager.Victory += EnableStatusScreen;

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

        if (_lives == 6)
        {
            foreach (GameObject blue in _blueUI)
            {
                blue.SetActive(false);
            }
            foreach (GameObject yellow in _yellowUI)
            {
                yellow.SetActive(true);
            }
        }

        else if (_lives == 3)
        {
            foreach (GameObject yellow in _yellowUI)
            {
                yellow.SetActive(false);
            }
            foreach (GameObject red in _redUI)
            {
                red.SetActive(true);
            }
        }

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
        if (gameHasStarted == true)
        {
            Time.timeScale = 1f;
        }

        else if (gameHasStarted == false)
        {
            EnableStatusScreen();
            StartCoroutine(Countdown());
            gameHasStarted = true;
        }
    }

    private void EnableStatusScreen()
    {
        _statusScreen.SetActive(true);
    }

    private void DisableStatusScreen()
    {
        _statusScreen.SetActive(false);
    }

    private IEnumerator Countdown()
    {
        int count = 5;
        while (count >= 1)
        {
            _statusScreenText.text = count.ToString();
            count--;
            yield return new WaitForSeconds(1);
        }
        DisableStatusScreen();
    }
}
