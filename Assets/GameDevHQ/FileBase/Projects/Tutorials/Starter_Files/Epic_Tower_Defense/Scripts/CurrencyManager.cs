using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoSingleton<CurrencyManager>
{
    [SerializeField] private int _initialWarFunds = 500;
    [SerializeField] private int _currentWarFunds;
    [SerializeField] private Text _totalWarFundsRef;
    [SerializeField] private Text _warFundsRef;

    private string _noFunds = "Need funds";
    private string _funds = "Good";
    public bool haveFunds = true;

    private IEnumerator coroutine;

    private void OnEnable()
    {
        _currentWarFunds = _initialWarFunds;
        _totalWarFundsRef.text = _currentWarFunds.ToString();
    }
    public void PayTower(int warfundsreq)
    {
        _initialWarFunds -= warfundsreq;
        _currentWarFunds = _initialWarFunds;
        _totalWarFundsRef.text = _currentWarFunds.ToString();

    }

    public bool HaveFunds(int warfundsReq)
    {


        if (_currentWarFunds < warfundsReq)
        {
            StartCoroutine(NotEnoughFunds());
        }

        return _currentWarFunds >= warfundsReq;
    }

    private IEnumerator NotEnoughFunds()
    {
        _warFundsRef.text = _noFunds.ToString();
        yield return new WaitForSeconds(2.5f);
        _warFundsRef.text = _funds;
    }

    
}
