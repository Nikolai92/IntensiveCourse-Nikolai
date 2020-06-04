using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRecycler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.SetActive(false);
        Debug.Log("IM COLLIDING");
    }
}
