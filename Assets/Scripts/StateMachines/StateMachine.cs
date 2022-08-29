using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State currentState;

   
    private void Update()
    {
        //adding a ? after the variable checks if its null, and if its not it will proceed
            currentState?.Tick(Time.deltaTime);

        //Debug.Log("Update being called"); // always return null in the console

    }

    public void SwitchState(State newState)
    {
       
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
       // Debug.Log(currentState); // correctly returns the state


    }
}
