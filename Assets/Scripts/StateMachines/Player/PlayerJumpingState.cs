using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private readonly int wallJumpHash = Animator.StringToHash("Braced Hang Hop Up");
    private const float CrossfadeDuration = 0.1f;
    private Vector3 momentum;
    private bool fromHanging = false;
    public PlayerJumpingState(PlayerStateMachine stateMachine,bool fromHanging) : base(stateMachine)
    {
        this.fromHanging = fromHanging;
    }

    public override void Enter()
    {
        
        stateMachine.LedgeDetector.OnLedgeDetect += HandLedgeDetect;
        if(!fromHanging)
        {
            stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);
            momentum = stateMachine.CharacterController.velocity;
            momentum.y = 0f;
            stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossfadeDuration);
        }
        else
        {
            stateMachine.ForceReceiver.Jump(stateMachine.JumpForce*1.5f);
            stateMachine.Animator.CrossFadeInFixedTime(wallJumpHash, CrossfadeDuration);
        }
        
    }

    public override void Tick(float deltatime)
    {
        Move(momentum, deltatime);
        if(stateMachine.CharacterController.velocity.y<=0)
        {
           stateMachine.fallingFromJumping = true;
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }
        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.LedgeDetector.OnLedgeDetect -= HandLedgeDetect;
    }

    private void HandLedgeDetect(Vector3 closestPoint, Vector3 ledgeForward, bool freeHanging, bool roomToClimbUp)
    {
        if (!stateMachine.WeaponActive && !stateMachine.wasHanging)
        {
            stateMachine.SwitchState(new PlayerHangingState(stateMachine, closestPoint, ledgeForward, freeHanging, roomToClimbUp));
        }
    }


}
