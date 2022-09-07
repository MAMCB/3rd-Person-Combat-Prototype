using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion-Blend Tree");
    private const float CrossfadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CrossfadeDuration);
    }

    public override void Tick(float deltatime)
    {
        
        
        if(!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }

        else if(IsInAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }

        MoveToPlayer(deltatime);
        FacePlayer();
        stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltatime);
        
    }

    

    public override void Exit()
    {
        stateMachine.NavMeshAgent.ResetPath();
        stateMachine.NavMeshAgent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float deltatime)
    {
        if(stateMachine.NavMeshAgent.isOnNavMesh)
        {
            stateMachine.NavMeshAgent.destination = stateMachine.Player.transform.position;
            Move(stateMachine.NavMeshAgent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltatime);
        }
       
        stateMachine.NavMeshAgent.velocity = stateMachine.CharacterController.velocity;
    }

    private bool IsInAttackRange()
    {
        if (stateMachine.Player.Isdead) { return false; }
        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }

}
