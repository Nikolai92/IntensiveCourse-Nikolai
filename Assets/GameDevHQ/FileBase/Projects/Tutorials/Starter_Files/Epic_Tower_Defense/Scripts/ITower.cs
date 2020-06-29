using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    int WarFundsRequired { get; set; }
    int TowerID { get; set; }

    GameObject CurrentTower { get; set; }
    GameObject UpgradedTowerObject { get; set; }
    Vector3 PlacedTowerPos { get; set; }

    void Attack();
}
