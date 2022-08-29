using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected bool IsInChaseRange()
    {
        float PlayerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return PlayerDistanceSqr <= stateMachine.PlayerDetectionRange* stateMachine.PlayerDetectionRange;
    }

    protected void Move(Vector3 motion, float deltatime)
    {
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltatime);
    }

    protected void Move(float deltatime)
    {
        Move(Vector3.zero, deltatime);
    }

}
