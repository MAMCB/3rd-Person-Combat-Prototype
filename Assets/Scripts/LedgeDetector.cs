using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    public event Action<Vector3, Vector3,bool,bool> OnLedgeDetect;
    public bool OnLimiter;
    public int limiterSide;
    public bool onLedge;
    
    
    private void OnTriggerEnter(Collider other)
    {
        onLedge = true;
        OnLedgeDetect?.Invoke(other.ClosestPointOnBounds(transform.position), other.transform.forward, other.GetComponent<Ledge>().freeHanging,other.GetComponent<Ledge>().roomToClimbUp);
        if(other.GetComponent<Limiter>())

        {
            OnLimiter = true;
           limiterSide= LimiterSide(other.GetComponent<Limiter>().CheckRightSideLimiter());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        onLedge = false;
        if (other.GetComponent<Limiter>())

        {
            OnLimiter = false;
        }

    }



    public int LimiterSide(bool limiterRight)
    {
        if (limiterRight)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}
