using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private const float CrossfadeDuration = 0.1f;
    private Vector3 momentum;
    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossfadeDuration);
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
        
    }

    
}
