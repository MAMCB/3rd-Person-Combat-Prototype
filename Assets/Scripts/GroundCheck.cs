using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] GameObject groundCheck;
    [SerializeField] float groundRange = 0.5f;
    [SerializeField] LayerMask groundLayer;
    public bool isInGroundRange;


    private void Update()
    {
        isInGroundRange = Physics.CheckSphere(groundCheck.transform.position, groundRange, groundLayer);
        
           
        
    }
}
