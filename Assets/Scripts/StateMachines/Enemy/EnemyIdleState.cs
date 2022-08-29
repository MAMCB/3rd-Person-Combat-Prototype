using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion-Blend Tree");
    private const float CrossfadeDuration = 0.2f;
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CrossfadeDuration);
        
    }

    public override void Tick(float deltatime)
    {
        
    }

    public override void Exit()
    {
        
    }

   
}
