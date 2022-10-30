using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDecalManager : MonoBehaviour
{
    public void IncreaseBloodStains()
    {
        foreach(Transform child in transform)
        {
            if(!child.gameObject.activeInHierarchy)
            {
                child.gameObject.SetActive(true);
                return;
            }
        }
    }
}
