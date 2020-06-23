using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoSingleton <CurrencyManager>
{
    [SerializeField] private int _initialWarFunds = 500;
    [SerializeField] private int _currentWarFunds;

    [SerializeField] private Text _totalWarFundsRef;
    [SerializeField] private Text _warFundsRef;
    private string _noFunds = "Not enough funds";

    public bool haveFunds = true;

    private void OnEnable()
    {
        _currentWarFunds = _initialWarFunds;
    }
    public void PayTower(int warfundsreq)
    {
        Debug.Log(_initialWarFunds);
        _initialWarFunds -= warfundsreq;
        _currentWarFunds = _initialWarFunds;
        _totalWarFundsRef.text = _currentWarFunds.ToString();
        Debug.Log(_currentWarFunds);     
    }

    public bool HaveFunds(int warfundsReq)
    {
        

        if (_currentWarFunds < warfundsReq)
        {
            _warFundsRef.text = _noFunds.ToString();
        }

        return _currentWarFunds > warfundsReq;
    }
}
