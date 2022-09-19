using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limiter : MonoBehaviour
{
    [SerializeField] bool limiterRight;

    public bool CheckRightSideLimiter()
    {
        return limiterRight;
    }
}
