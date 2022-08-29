using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyBaseState stateMachine;

    public EnemyBaseState(EnemyBaseState stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    
}
