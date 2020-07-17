using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWave.asset", menuName = "ScritpableObj/Wave")]
public class Wave : ScriptableObject
{
    public List<GameObject> sequence;
}
