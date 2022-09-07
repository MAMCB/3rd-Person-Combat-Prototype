using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");
    private const float CrossfadeDuration = 0.1f;
    private Vector3 momentum;
    private readonly int AxeFallAttack = Animator.StringToHash("AxeFallingAttack");
    private readonly int SwordFallAttack = Animator.StringToHash("SwordFallingAttack");
    private bool playedAttackAnimation = false;
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
        stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossfadeDuration);
    }

   

    public override void Tick(float deltatime)
    {
       if(!stateMachine.CharacterController.isGrounded)
        {
            Move(momentum, deltatime);
        }
       else
        {
            Move(deltatime);
        }
        
        if (stateMachine.InputReader.IsAttacking && !playedAttackAnimation)
        {
            if(!stateMachine.WeaponActive) { return; }
            if(stateMachine.SwordActive)
            {
                playedAttackAnimation = true;
                stateMachine.WeaponDamageSword.SetAttack(stateMachine.swordFallDamage, stateMachine.swordFallKnockbackForce);
                stateMachine.Animator.CrossFadeInFixedTime(SwordFallAttack, CrossfadeDuration);
            }
            if(stateMachine.AxeActive)
            {
                playedAttackAnimation = true;
                stateMachine.WeaponDamageAxe.SetAttack(stateMachine.axeFallDamage, stateMachine.axeFallKnockbackForce);
                stateMachine.Animator.CrossFadeInFixedTime(AxeFallAttack, CrossfadeDuration);
            }
        }
        CheckAnimationDuration();


            if (stateMachine.CharacterController.isGrounded && !playedAttackAnimation)
        {
            ReturnToLocomotion();
            
        }
        FaceTarget();
    }


    public override void Exit()
    {
        stateMachine.fallingFromJumping = false;
    }

    private void CheckAnimationDuration()
    {
       if( GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            playedAttackAnimation = false;
        }
    }
}
