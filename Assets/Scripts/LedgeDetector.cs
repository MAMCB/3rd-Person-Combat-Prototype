using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    public event Action<Vector3, Vector3,bool> OnLedgeDetect;
    private void OnTriggerEnter(Collider other)
    {
        OnLedgeDetect?.Invoke(other.ClosestPoint(transform.position), other.transform.forward, other.GetComponent<Ledge>().freeHanging);
    }
}
