using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

        Debug.Log("Enemy is Dead");
        stateMachine.Weapon.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);
        stateMachine.Ragdoll.ToggleRagdoll(true);
    }

    public override void Tick(float deltatime)
    {
        
    }

    public override void Exit()
    {
        
    }

    
}
