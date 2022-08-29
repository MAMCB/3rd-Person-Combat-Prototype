using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWithdrawWeapon : PlayerBaseState
{
    private string animationName;
    public PlayerWithdrawWeapon(PlayerStateMachine stateMachine, int weaponNumber) : base(stateMachine)
    {
        if(weaponNumber==1)
        {
            animationName = stateMachine.WithdrawingSword.AnimationName;
        }
        if (weaponNumber==2)
        {
            animationName = stateMachine.WithdrawingAxe.AnimationName;
        }
    }

    public override void Enter()
    {
        
        stateMachine.Animator.CrossFadeInFixedTime(animationName, 0.1f);
    }

    public override void Tick(float deltatime)
    {
        if(!stateMachine.InputReader.Weapon1 && !stateMachine.InputReader.Weapon2)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }

   

    
}
