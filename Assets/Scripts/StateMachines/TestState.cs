using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : PlayerBaseState
{
    public TestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("entered test state");// correctly logs in the message
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltatime)
    {
        Debug.Log("Test state ticking"); // never logs the message in.
    }
}
