using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }

    [field: SerializeField] public Target Target { get; private set; }

    [field: SerializeField] public CharacterController CharacterController { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public float PlayerDetectionRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }

    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public float AttackKnockback { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }

    public GameObject Player { get; private set; }
    public bool AttackMissed;

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDeath;
    }

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

    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactState(this));
    }

    private void HandleDeath()
    {
        SwitchState(new EnemyDeadState(this));
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDeath;
    }
}