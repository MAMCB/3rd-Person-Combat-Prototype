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
        if (stateMachine.Player.Isdead) { return false; }
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

    protected void FacePlayer()
    {
        if (stateMachine.Player == null) { return; }
        Vector3 TargetDirection = stateMachine.Player.transform.position - stateMachine.transform.position;

        TargetDirection.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(TargetDirection);
    }

}
