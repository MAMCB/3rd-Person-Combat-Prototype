using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

    [field: SerializeField] public CharacterController CharacterController { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public float PlayerDetectionRange { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }

    public GameObject Player { get; private set; }

    void Start()
    {
        Player=GameObject.FindGameObjectWithTag("Player");
        NavMeshAgent.updatePosition = false; // so the navmesh agent doesn't move the character
        NavMeshAgent.updateRotation = false;
        SwitchState(new EnemyIdleState(this));

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectionRange);
    }
}
