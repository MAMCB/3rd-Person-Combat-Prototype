using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;
    private Attack attack;
    
    private string Sword= "Sword";
    private string Axe = "Axe";


    public PlayerAttackingState(PlayerStateMachine stateMachine, int AttackIndex) : base(stateMachine)
    {
        
        

        if (stateMachine.LookValue == Vector2.zero)
        {
            if (Sword == stateMachine.currentWeapon.WeaponName)
            {
                attack = stateMachine.SwordAttacks[AttackIndex];
            }
            if (Axe == stateMachine.currentWeapon.WeaponName)
            {
                attack = stateMachine.AxeAttacks[AttackIndex];
            }


        }
        else if (stateMachine.LookValue.x > 0 || stateMachine.LookValue.y > 0)
        {
            if (Sword == stateMachine.currentWeapon.WeaponName)
            {
                attack = stateMachine.HighSwordAttack;
            }

            if (Axe == stateMachine.currentWeapon.WeaponName)
            {
                attack = stateMachine.HighAxeAttack;

            }
        }
        else if (stateMachine.LookValue.x < 0 || stateMachine.LookValue.y < 0)
        {
            if (Sword == stateMachine.currentWeapon.WeaponName)
            {
                attack = stateMachine.LowSwordAttack;
            }
            if (Axe == stateMachine.currentWeapon.WeaponName)
            {
                attack = stateMachine.LowAxeAttack;

            }

        }
        
    }

    public override void Enter()
    {
        if (Sword == stateMachine.currentWeapon.WeaponName)
        {
            stateMachine.WeaponDamageSword.SetAttack(attack.Damage);
        }

        if (Axe == stateMachine.currentWeapon.WeaponName)
        {
            stateMachine.WeaponDamageAxe.SetAttack(attack.Damage);
        }


            stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName,attack.TransitionDuration);
        
    }

   

    public override void Tick(float deltatime)
    {
        Move(deltatime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime();

        if(normalizedTime>=previousFrameTime && normalizedTime<1f)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if(stateMachine.Targeter.currentTarget!=null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }

       
        previousFrameTime = normalizedTime;

        

    }

   

    public override void Exit()
    {
        stateMachine.LookValue = Vector2.zero;

    }

    

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo= stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo= stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if(stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }

        else if(!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    private void TryComboAttack(float normalizedTime)
    {
        if(attack.ComboStateIndex==-1) { return; }
        if(normalizedTime<attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboStateIndex));
    }

    private void TryApplyForce()
    {
        if(alreadyAppliedForce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
        alreadyAppliedForce = true;
    }


}
