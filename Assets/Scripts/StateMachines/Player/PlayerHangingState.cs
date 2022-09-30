using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private bool freeHanging;
    private bool roomToClimbUp;
    private Vector3 closestPoint;
    private Vector3 ledgeForward;
   
    private readonly int freeHangingBlendTreeHash = Animator.StringToHash("FreeHang Climbing BlendTree");
    private readonly int shimmyRightHash = Animator.StringToHash("ShimmyRight");
    private readonly int bracedHangingBlendTreeHash = Animator.StringToHash("BracedHang Climbing BlendTree ");
    private const float CrossfadeDuration = 0.1f;

    public PlayerHangingState(PlayerStateMachine stateMachine,Vector3 closestPoint, Vector3 ledgeForward, bool freeHanging, bool roomToClimbUp) : base(stateMachine)
    {
        this.freeHanging = freeHanging;
        this.closestPoint = closestPoint;
        this.roomToClimbUp = roomToClimbUp;
        if(freeHanging)
        {
            this.closestPoint.y = closestPoint.y + stateMachine.freeHangGrabPointOffset;
        }
        else
        {
            this.closestPoint.y = closestPoint.y + stateMachine.wallGrabPointOffset;
       }
        
        this.ledgeForward = ledgeForward;
    }

    public override void Enter()
    {
        stateMachine.wasHanging = true;
        stateMachine.FallVelocity = 0f;
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);
        stateMachine.CharacterController.enabled = false;
       stateMachine.transform.position = closestPoint-(stateMachine.LedgeDetector.transform.position-stateMachine.transform.position);
        stateMachine.CharacterController.enabled = true;
        if(freeHanging)
        {
            stateMachine.Animator.CrossFadeInFixedTime(freeHangingBlendTreeHash, CrossfadeDuration);
            
        }
        else
        {
            stateMachine.Animator.CrossFadeInFixedTime(bracedHangingBlendTreeHash, CrossfadeDuration);
            stateMachine.InputReader.JumpEvent += OnJump;
        }
        stateMachine.LedgeDetector.OnLedgeDetect += HandLedgeDetect;
    }

    public override void Tick(float deltatime)
    {
        if(stateMachine.InputReader.MovementValue.y<0)
        {
            stateMachine.CharacterController.Move(Vector3.zero);
            stateMachine.ForceReceiver.Reset();
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }
        if(stateMachine.InputReader.MovementValue.y>0)
        {
            if(!roomToClimbUp) { return; }
            stateMachine.SwitchState(new PlayerPullUpState(stateMachine,freeHanging));
        }
        Vector3 movement = CalculateMovement(deltatime);
        if (stateMachine.LedgeDetector.OnLimiter && stateMachine.LedgeDetector.limiterSide == 1 && movement.x > 0)
        {
            Debug.Log("canno't move right");
            return;
        }
        if (stateMachine.LedgeDetector.OnLimiter && stateMachine.LedgeDetector.limiterSide == -1 && movement.x < 0)
        {
            Debug.Log("canno't move left");
            return;
        }

        HangMove(movement * stateMachine.HangingMovementSpeed, deltatime);
        UpdateAnimator(deltatime);
       
    }

    public override void Exit()
    {
        if(!freeHanging)
        {
            stateMachine.InputReader.JumpEvent -= OnJump;
        }
        stateMachine.LedgeDetector.OnLedgeDetect -= HandLedgeDetect;
    }

    private Vector3 CalculateMovement(float deltatime)
    {
        Vector3 movement = new Vector3();

       

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
       // movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;



        return movement;
    }

    private void UpdateAnimator(float deltatime)
    {
        float movingValueX = stateMachine.InputReader.MovementValue.x;
       // float movingValueY = stateMachine.InputReader.MovementValue.y;

        stateMachine.Animator.SetFloat(shimmyRightHash, movingValueX, 0.1f, deltatime);
       // stateMachine.Animator.SetFloat(TargetingRightHash, movingValueY, 0.1f, deltatime);


    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine,true));
    }

    private void HandLedgeDetect(Vector3 closestPoint, Vector3 ledgeForward, bool freeHanging, bool roomToClimbUp)
    {
       
            stateMachine.SwitchState(new PlayerHangingState(stateMachine, closestPoint, ledgeForward, freeHanging, roomToClimbUp));
        

    }
}
