using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("EnemyAttack");
    private const float CrossfadeDuration = 0.1f;
    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback) ;
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, CrossfadeDuration);
    }

   

    public override void Tick(float deltatime)
    {
        FacePlayer();
        if (GetNormalizedTime(stateMachine.Animator)>=1) // if the animation is finished
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
        if(stateMachine.AttackMissed)
        {
            //stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
        
    }

    public override void Exit()
    {
        stateMachine.AttackMissed = false;
    }
}
