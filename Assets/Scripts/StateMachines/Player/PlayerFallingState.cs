using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");
    private readonly int FallingHash = Animator.StringToHash("Falling");
    private const float CrossfadeDuration = 0.1f;
    private Vector3 momentum;
    private readonly int AxeFallAttack = Animator.StringToHash("AxeFallingAttack");
    private readonly int SwordFallAttack = Animator.StringToHash("SwordFallingAttack");
    private bool playedAttackAnimation = false;
    //private float fallTimer = 0.5f;
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
        stateMachine.LedgeDetector.OnLedgeDetect += HandLedgeDetect;
        if(stateMachine.groundCheck.isInGroundRange)
        {
            stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossfadeDuration);
        }
        else
        {
            stateMachine.Animator.CrossFadeInFixedTime(FallingHash, CrossfadeDuration);
        }
        
        
    }

   

    public override void Tick(float deltatime)
    {
        //if(stateMachine.groundCheck.isInGroundRange)
        //{
        //    stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossfadeDuration);
        //}
        
       if(!stateMachine.CharacterController.isGrounded)
        {
            Move(momentum, deltatime);
            stateMachine.FallVelocity += deltatime;
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

            if(stateMachine.CharacterController.isGrounded)  
        {
            if(stateMachine.FallVelocity > stateMachine.maximumFallVelocity)
            {
                stateMachine.Health.DealDamage(100);
            }
            else
            {
                stateMachine.FallVelocity = 0f;
            }
            
        }
          

        FaceTarget();
    }


    public override void Exit()
    {
        stateMachine.wasHanging = false;
        stateMachine.LedgeDetector.OnLedgeDetect -= HandLedgeDetect;
    }

    private void CheckAnimationDuration()
    {
       if( GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            playedAttackAnimation = false;
        }
    }

    private void HandLedgeDetect(Vector3 closestPoint,Vector3 ledgeForward,bool freeHanging)
    {
        if(!stateMachine.WeaponActive && !stateMachine.wasHanging)
        {
            stateMachine.SwitchState(new PlayerHangingState(stateMachine, closestPoint, ledgeForward, freeHanging));
        }
       
    }
}
