using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogAndFire : MonoBehaviour
{
    [SerializeField] GameObject fires;
    [SerializeField] GameObject fog;
    

    // Update is called once per frame
    void Update()
    {
        if(fog.activeInHierarchy)
        {
            fires.SetActive(true);
        }
        else
        {
            fires.SetActive(false);
        }
    }
}
