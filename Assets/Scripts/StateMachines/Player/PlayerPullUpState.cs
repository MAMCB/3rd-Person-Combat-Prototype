using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    private readonly int freeHangingClimbHash = Animator.StringToHash("Freehang Climb");
    private readonly int bracedHangingClimbHash = Animator.StringToHash("Braced Hang pull up");
    private const float CrossfadeDuration = 0.1f;
    private readonly Vector3 Offset = new Vector3(0f, 2.325f, 0.65f);
    private bool freehanging;
    public PlayerPullUpState(PlayerStateMachine stateMachine,bool freehanging) : base(stateMachine)
    {
        this.freehanging = freehanging;
    }

    public override void Enter()
    {
        if(freehanging)
        {
            stateMachine.Animator.CrossFadeInFixedTime(freeHangingClimbHash, CrossfadeDuration);
        }
        else
        {
            stateMachine.Animator.CrossFadeInFixedTime(bracedHangingClimbHash, CrossfadeDuration);
        }
       
    }

    

    public override void Tick(float deltatime)
    {
        if (GetNormalizedTime(stateMachine.Animator, "Climbing") <1f) { return; }

        stateMachine.CharacterController.enabled = false;
        stateMachine.transform.Translate(Offset,Space.Self);
        stateMachine.CharacterController.enabled = true;
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine,false));
        
    }

    public override void Exit()
    {
        stateMachine.CharacterController.Move(Vector3.zero);
        stateMachine.ForceReceiver.Reset();
    }
}
