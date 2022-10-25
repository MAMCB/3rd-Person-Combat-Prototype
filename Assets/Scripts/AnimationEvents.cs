using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] PlayerStateMachine stateMachine;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject sword2;
    [SerializeField] GameObject Axe1;
    [SerializeField] GameObject Axe2;
    [SerializeField] AudioClip swordSheatsound;
    [SerializeField] AudioClip[] footsteps;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySwordSheatSoundevent()
    {
        audioSource.PlayOneShot(swordSheatsound);
    }
    public void SwordWithdrawingEvent()
    {
        stateMachine.InputReader.Weapon1 = false;
        stateMachine.SwordActive = true;
        
    }

    public void ActivateSwordEvent()
    {
        sword.SetActive(true);
        sword2.SetActive(false);
    }

    public void SwordSheatingEvent()
    {
        stateMachine.SwordActive = false;
        sword.SetActive(false);
        sword2.SetActive(true);
        stateMachine.InputReader.SheatWeapon = false;
    }

    public void ActivateAxeEvent()
    {
        Axe1.SetActive(true);
        Axe2.SetActive(false);
    }

    public void AxeDrawingEvent()
    {
        stateMachine.InputReader.Weapon2 = false;
        stateMachine.AxeActive = true;
    }

    public void AxeSheatingEvent()
    {
        stateMachine.AxeActive = false;
        Axe1.SetActive(false);
        Axe2.SetActive(true);
        stateMachine.InputReader.SheatWeapon = false;

    }

    public void PlayFootstepsEvent()
    {
        audioSource.clip = footsteps[Random.Range(0, footsteps.Length)];
        audioSource.Play();
    }
}
