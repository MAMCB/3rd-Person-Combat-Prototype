using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeTransition : MonoBehaviour
{
    public bool freehanging;
    public bool spaceToClimbUp;
    [Range(-1,1)]
    public int transitionDirection;
}
