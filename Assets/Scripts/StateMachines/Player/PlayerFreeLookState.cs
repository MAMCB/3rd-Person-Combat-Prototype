using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{

    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("Free Look Blend Tree");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossfadeDuration = 0.2f;
    private float freeLookMovementSpeed;
    private float animatorFloatValue;
    
    
    
    
    
    
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }


    public override void Enter()
    {
        //Debug.Log("Entered Free Look State");
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossfadeDuration);
        stateMachine.isBattleSoundPlaying = false;

        if (!stateMachine.isAmbientSoundPlaying)
        {
            stateMachine.audioSource.clip = stateMachine.ambientSound;
            stateMachine.audioSource.Play();
            stateMachine.isAmbientSoundPlaying = true;

        }
            
        stateMachine.InputReader.TargetEvent += OnTarget;
        
        
        freeLookMovementSpeed = stateMachine.FreeLookMovementWalkingSpeed;
        
        

    }

    public override void Tick(float deltatime)
    {
       // Debug.Log("ticking");
        CheckWeapon1Input();
        CheckWeapon2Input();

        CheckSheatWeaponInput();
        if (stateMachine.InputReader.IsAttacking && stateMachine.WeaponActive)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
        Vector3 movement = CalculateMovement();
        Move(movement * freeLookMovementSpeed, deltatime);


        

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            animatorFloatValue = 0f;
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, animatorFloatValue, AnimatorDampTime, deltatime);

            return;

        }
        if (stateMachine.InputReader.IsSprinting)
        {
            animatorFloatValue = 1f;
            freeLookMovementSpeed = stateMachine.FreeLookMovementRunningSpeed;
        }
        else if (!stateMachine.InputReader.IsSprinting)
        {
            animatorFloatValue = 0.5f;
            freeLookMovementSpeed = stateMachine.FreeLookMovementWalkingSpeed;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, animatorFloatValue, AnimatorDampTime, deltatime);
        FaceMovementDirection(movement, deltatime);


    }

    private void CheckSheatWeaponInput()
    {
        if (stateMachine.InputReader.SheatWeapon && stateMachine.WeaponActive)
        {
            if (stateMachine.SwordActive)
            {
                stateMachine.SwitchState(new PlayerSheatWeapon(stateMachine,1,false));
            }

            if (stateMachine.AxeActive)
            {
                stateMachine.SwitchState(new PlayerSheatWeapon(stateMachine,2,false));
            }

        }

        else
        {
            stateMachine.InputReader.SheatWeapon = false;
        }
    }

    private void CheckWeapon2Input()
    {
        if(!stateMachine.SwordActive)
        {
            if (stateMachine.InputReader.Weapon2 && !stateMachine.AxeActive)
            {
                stateMachine.currentWeapon = stateMachine.Weapon2;
                stateMachine.SwitchState(new PlayerWithdrawWeapon(stateMachine, 2));
            }
            else
            {
                stateMachine.InputReader.Weapon2 = false;
            }
        }
        else
        {
            if (stateMachine.InputReader.Weapon2 && !stateMachine.AxeActive)
            {
                stateMachine.currentWeapon = stateMachine.Weapon2;
                stateMachine.InputReader.SheatWeapon = true;
                stateMachine.SwitchState(new PlayerSheatWeapon(stateMachine, 1, true));
            }
            else
            {
                stateMachine.InputReader.Weapon2 = false;
            }
        }
        
    }

    private void CheckWeapon1Input()
    {
        if(!stateMachine.AxeActive)
        {
            if (stateMachine.InputReader.Weapon1 && !stateMachine.SwordActive)
            {
                stateMachine.currentWeapon = stateMachine.Weapon1;
                stateMachine.SwitchState(new PlayerWithdrawWeapon(stateMachine, 1));
            }
            else
            {
                stateMachine.InputReader.Weapon1 = false;
            }

        }
        if(stateMachine.AxeActive)
        {
            if (stateMachine.InputReader.Weapon1 && !stateMachine.SwordActive)
            {
                stateMachine.currentWeapon = stateMachine.Weapon1;
                stateMachine.InputReader.SheatWeapon = true;
                stateMachine.SwitchState(new PlayerSheatWeapon(stateMachine, 2, true));
            }
            else
            {
                stateMachine.InputReader.Weapon1 = false;
            }

        }
       
    }


    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        
        

    }

    private void OnTarget()
    {
        if(!stateMachine.Targeter.SelectTarget() || !stateMachine.WeaponActive) { return; }

        stateMachine.isAmbientSoundPlaying = false;
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        
        
    }

    

    

    private Vector3 CalculateMovement()
    {
        
        Vector3 forward=stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
    }

    private void FaceMovementDirection(Vector3 movement, float deltatime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltatime * stateMachine.RotationSmoothValue);
    }



}
