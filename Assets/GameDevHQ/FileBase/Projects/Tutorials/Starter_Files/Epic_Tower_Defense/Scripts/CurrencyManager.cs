using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoSingleton <CurrencyManager>
{
    [SerializeField] private int _initialWarFunds = 500;
    [SerializeField] private int _currentWarFunds;

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
        Debug.Log(_currentWarFunds);     
    }

    public bool HaveFunds(int warfundsReq)
    {
        if (_currentWarFunds > warfundsReq)
        {
            haveFunds = true;
            return haveFunds;
        }

        else 
        {
            haveFunds = false;
            return haveFunds;
        }
    }
}
