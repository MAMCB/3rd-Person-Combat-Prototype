using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("ImpactPlayer");
    private const float CrossfadeDuration = 0.1f;
    private float duration = 1f;
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossfadeDuration);
    }

   

    public override void Tick(float deltatime)
    {
        Move(deltatime);
        duration -= deltatime;
        if(duration<=0f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        
    }
}
