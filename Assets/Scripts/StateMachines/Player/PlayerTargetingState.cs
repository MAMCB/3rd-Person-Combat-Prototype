using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("Targeting Blend Tree");

    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");
    private const float CrossfadeDuration = 0.1f;
    
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossfadeDuration);
        stateMachine.InputReader.TargetEvent += OnTarget;
        //stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.JumpEvent += OnJump;
        if (!stateMachine.isBattleSoundPlaying)
        {
            stateMachine.audioSource.clip = stateMachine.battleSound;
            stateMachine.audioSource.Play();
            stateMachine.isBattleSoundPlaying = true;
        }
       
    }

    public override void Tick(float deltatime)
    {
        if(stateMachine.InputReader.IsAttacking)
        {
            stateMachine.LookValue = stateMachine.InputReader.LookValue;
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,0));
            return;
        }

        if(stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }
        if(stateMachine.Targeter.currentTarget==null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        Vector3 movement = CalculateMovement(deltatime);
        Move(movement * stateMachine.TargetingMovementSpeed, deltatime);
        UpdateAnimator(deltatime);
        FaceTarget();
        if(stateMachine.InputReader.SheatWeapon)
        {
            stateMachine.InputReader.SheatWeapon = false;
        }

        if(stateMachine.InputReader.ScrollValue.y>0f)
        {
            stateMachine.Targeter.NextTarget();
        }

        if (stateMachine.InputReader.ScrollValue.y < 0f)
        {
            stateMachine.Targeter.PreviousTarget();
        }

    }

    

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        //stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.JumpEvent -= OnJump;


    }

    private void OnTarget()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    //private void OnDodge()
    //{
    //    if(stateMachine.InputReader.MovementValue==Vector2.zero) { return; }
    //    stateMachine.SwitchState(new PlayerDodgeState(stateMachine,stateMachine.InputReader.MovementValue));
        
    //}

    private void OnJump()
    {
        // stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        if (stateMachine.InputReader.MovementValue == Vector2.zero) { return; }
        stateMachine.SwitchState(new PlayerDodgeState(stateMachine, stateMachine.InputReader.MovementValue));
    }
    private Vector3 CalculateMovement(float deltatime)
    {
        Vector3 movement = new Vector3();

        
            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        

       
        return movement;
    }

    private void UpdateAnimator(float deltatime)
    {
        float movingValueX = stateMachine.InputReader.MovementValue.x;
        float movingValueY= stateMachine.InputReader.MovementValue.y;

        stateMachine.Animator.SetFloat(TargetingForwardHash, movingValueX,0.1f,deltatime);
        stateMachine.Animator.SetFloat(TargetingRightHash, movingValueY, 0.1f, deltatime);


    }


}
