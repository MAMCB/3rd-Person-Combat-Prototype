using System.Collections;
using System.Collections.Generic;
using System; // to be able to use action Events
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; } // public for reading but private for setting
    public Vector2 LookValue { get; private set; }
    public event Action JumpEvent;
    public bool Weapon1;
    public bool Weapon2;
    public bool SheatWeapon;
    
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action CancelEvent;
    public  bool IsAttacking { get; private set; }
    public bool IsSprinting { get; private set; }
    private Controls controls;
   private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookValue= context.ReadValue<Vector2>();
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        TargetEvent?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        CancelEvent?.Invoke();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsSprinting = true;
        }
        else if (context.canceled)
        {
            IsSprinting = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            IsAttacking = true;
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnWeapon1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Weapon1 = true;

        }
    }

    public void OnWeapon2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Weapon2 = true;

        }
    }

    public void OnSheatWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SheatWeapon = true;
        }
    }
}
