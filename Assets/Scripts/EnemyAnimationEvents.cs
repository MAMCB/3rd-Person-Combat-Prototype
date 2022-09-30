using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip attackRoar;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

   public void AttackSoundEvent()
    {
        audioSource.PlayOneShot(attackRoar);
    }
}
