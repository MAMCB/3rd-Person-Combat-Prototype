using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float drag = 0.3f;

    private Vector3 Impact;
    private Vector3 DampingVelocity;
    private float VerticalVelocity;
    public Vector3 Movement =>Impact+ Vector3.up * VerticalVelocity;

    private void Update()
    {
        if(VerticalVelocity<0f && controller.isGrounded)
        {
            VerticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            VerticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        Impact = Vector3.SmoothDamp(Impact, Vector3.zero, ref DampingVelocity, drag);
        if (agent != null)
        {
            if (Impact.sqrMagnitude < 0.2f*0.2f)
            {
                Impact = Vector3.zero;
                agent.enabled = true;
            }
        }
    }

    public void AddForce(Vector3 force)
    {
        Impact += force;
        if(agent!=null)
        {
            agent.enabled = false;
        }
    }
}
