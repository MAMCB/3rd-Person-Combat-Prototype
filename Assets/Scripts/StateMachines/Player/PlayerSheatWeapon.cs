using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSheatWeapon : PlayerBaseState
{
    private string animationName;
    private bool Axe;
    private bool Sword;
    private bool ImediatlyDrawOtherWeapon;
    private int WeaponNumber;
    public PlayerSheatWeapon(PlayerStateMachine stateMachine, int weaponNumber, bool otherWeapon) : base(stateMachine)
    {
        if (weaponNumber == 1)
        {
            animationName = stateMachine.SheatSword.AnimationName;
            Sword = true;
            WeaponNumber = 2;
        }

        if (weaponNumber ==2)
        {
            animationName = stateMachine.SheatAxe.AnimationName;
            Axe = true;
            WeaponNumber = 1;
        }

        ImediatlyDrawOtherWeapon = otherWeapon;
        

        
    }

    public override void Enter()
    {
     if(Sword)
        {
            stateMachine.Animator.CrossFadeInFixedTime(animationName, 0.1f);
            
        }
     if(Axe)
        {
            stateMachine.Animator.CrossFadeInFixedTime(animationName, 0.4f);
            
        }
        
        
    }

    

   

    public override void Tick(float deltatime)
    {
        if (!stateMachine.InputReader.SheatWeapon)
        {



            if (ImediatlyDrawOtherWeapon)
            {
                stateMachine.SwitchState(new PlayerWithdrawWeapon(stateMachine, WeaponNumber));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }


        }
        
    }

    public override void Exit()
    {
        Sword = false;
        Axe = false;
        ImediatlyDrawOtherWeapon = false;
    }
}
