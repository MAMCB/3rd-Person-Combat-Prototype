using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private Vector2 dodgingDirectionInput;
    private float remainingDodgeTime;
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
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.InputReader.DodgeEvent += OnDodge;
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

        

    }

    

    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.JumpEvent -= OnJump;


    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private void OnDodge()
    {
        if(Time.time-stateMachine.PreviousDodgeTime<stateMachine.DodgeCoolDown) { return; }

        stateMachine.SetDodgeTime(Time.time);
        dodgingDirectionInput = stateMachine.InputReader.MovementValue;
        remainingDodgeTime = stateMachine.DodgeDuration;
        
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }
    private Vector3 CalculateMovement(float deltatime)
    {
        Vector3 movement = new Vector3();

        if(remainingDodgeTime>0f)
        {
            movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeLenght/stateMachine.DodgeDuration;
            movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeLenght / stateMachine.DodgeDuration;
            remainingDodgeTime = MathF.Max(remainingDodgeTime - deltatime, 0f);
        }
        else
        {
            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        }

       
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
