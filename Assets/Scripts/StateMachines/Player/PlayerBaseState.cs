using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;
   

    public PlayerBaseState(PlayerStateMachine stateMachine) //constructor of the class
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltatime)
    {
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltatime);
    }

    protected void Move(float deltatime)
    {
        Move(Vector3.zero, deltatime);
    }

    protected void FaceTarget()
    {
        if (stateMachine.Targeter.currentTarget == null) { return; }
       Vector3  TargetDirection = stateMachine.Targeter.currentTarget.transform.position - stateMachine.transform.position;
       
        TargetDirection.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(TargetDirection);
    }

    protected void ReturnToLocomotion()
    {
        if(stateMachine.Targeter.currentTarget!=null)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}
