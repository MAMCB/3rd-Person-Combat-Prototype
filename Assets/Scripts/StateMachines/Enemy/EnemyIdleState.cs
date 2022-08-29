using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion-Blend Tree");
    private const float CrossfadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CrossfadeDuration);
        
    }

    public override void Tick(float deltatime)
    {
        Move(deltatime);
        if(IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
        stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDampTime, deltatime);
    }

    public override void Exit()
    {
        
    }

   
}
