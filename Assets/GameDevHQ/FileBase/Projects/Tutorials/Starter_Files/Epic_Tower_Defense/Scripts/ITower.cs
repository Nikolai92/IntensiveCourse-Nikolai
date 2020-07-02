using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    int WarFundsRequired { get; }
    int TowerID { get; }
    int UpgradeCost { get; }

    GameObject CurrentTowerObject { get; set; }
    GameObject UpgradedTowerObject { get; }
    Vector3 PlacedTowerPos { get; set; }

    void Attack();
}
