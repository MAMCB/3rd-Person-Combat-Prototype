using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private bool freeHanging;
    private Vector3 closestPoint;
    private Vector3 ledgeForward;
    private readonly int freeHangingHash = Animator.StringToHash("Hanging Idle-no Wall");
    private readonly int wallHangingHash = Animator.StringToHash("Hanging Idle-Wall");
    private const float CrossfadeDuration = 0.1f;

    public PlayerHangingState(PlayerStateMachine stateMachine,Vector3 closestPoint, Vector3 ledgeForward, bool freeHanging) : base(stateMachine)
    {
        this.freeHanging = freeHanging;
        this.closestPoint = closestPoint;
        if(freeHanging)
        {
            this.closestPoint.y = closestPoint.y - stateMachine.freeHangGrabPointOffset;
        }
        else
        {
            this.closestPoint.y = closestPoint.y - stateMachine.wallGrabPointOffset;
        }
        
        this.ledgeForward = ledgeForward;
    }

    public override void Enter()
    {
        stateMachine.wasHanging = true;
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);
       stateMachine.transform.position = closestPoint;
        if(freeHanging)
        {
            stateMachine.Animator.CrossFadeInFixedTime(freeHangingHash, CrossfadeDuration);
        }
        else
        {
            stateMachine.Animator.CrossFadeInFixedTime(wallHangingHash, CrossfadeDuration);
        }
    }

    public override void Tick(float deltatime)
    {
        if(stateMachine.InputReader.MovementValue.y<0)
        {
            stateMachine.CharacterController.Move(Vector3.zero);
            stateMachine.ForceReceiver.Reset();
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }

    
}
