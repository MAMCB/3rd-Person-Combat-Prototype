using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject lockOnFire;
    public event Action<Target> OnDestroyed;
    
    

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
        DeactivateLockOn();
    }
    
    public void ActivateLockOn()
    {
        lockOnFire.SetActive(true);  
        
    }

    public void DeactivateLockOn()
    {
        lockOnFire.SetActive(false);
        
    }
}
