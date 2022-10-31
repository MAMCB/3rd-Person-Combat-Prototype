using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private readonly int BlockHash = Animator.StringToHash("ShieldBlock");
    private const float CrossfadeDuration = 0.1f;
    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(BlockHash, CrossfadeDuration);
        stateMachine.Health.SetInvulnerable(true);
    }

    public override void Tick(float deltatime)
    {
        Move(deltatime);
        FaceTarget();
        if (!stateMachine.InputReader.IsBlocking)
        {
            ReturnToLocomotion();
            //stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            //return;
        }

        //if(stateMachine.Targeter.currentTarget==null)
        //{
        //    stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        //    return;
        //}
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }

    

   
}
