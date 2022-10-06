using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Player is Dead");
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.WeaponDamageSword.gameObject.SetActive(false);
        stateMachine.WeaponDamageAxe.gameObject.SetActive(false);
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.audioSource.clip = stateMachine.deathSound;
        stateMachine.audioSource.Play();
    }

    public override void Tick(float deltatime)
    {

    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnJump()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
