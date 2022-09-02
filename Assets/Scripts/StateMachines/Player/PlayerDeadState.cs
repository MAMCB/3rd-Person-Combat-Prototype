using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Player is Dead");
        stateMachine.WeaponDamageSword.gameObject.SetActive(false);
        stateMachine.WeaponDamageAxe.gameObject.SetActive(false);
        stateMachine.Ragdoll.ToggleRagdoll(true);
    }

    public override void Tick(float deltatime)
    {
        
    }

    public override void Exit()
    {
        
    }

    
}
