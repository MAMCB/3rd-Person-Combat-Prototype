using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private readonly int DodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");

    private readonly int DodgeForwardHash = Animator.StringToHash("DodgeForward");
    private readonly int DodgeRightHash = Animator.StringToHash("DodgeRight");
    private const float CrossfadeDuration = 0.1f;
    private Vector3 dodgingDirectionInput;
    private float remainingDodgeTime;
    public PlayerDodgeState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
    {
        this.dodgingDirectionInput = dodgingDirectionInput;
    }

    public override void Enter()
    {
        remainingDodgeTime = stateMachine.DodgeDuration;
        stateMachine.Animator.SetFloat(DodgeForwardHash, dodgingDirectionInput.y);
        stateMachine.Animator.SetFloat(DodgeRightHash, dodgingDirectionInput.x);
        stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, CrossfadeDuration);
        stateMachine.Health.SetInvulnerable(true);
    }

    public override void Tick(float deltatime)
    {
        Vector3 movement = new Vector3();
        movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeLenght / stateMachine.DodgeDuration;
        movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeLenght / stateMachine.DodgeDuration;
        Move(movement, deltatime);
        FaceTarget();
        remainingDodgeTime -= deltatime;

        if(remainingDodgeTime<=0f)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }

    
}
