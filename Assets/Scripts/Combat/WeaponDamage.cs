using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] AudioClip[] missSounds;
    [SerializeField] AudioSource audioSource;
    private int damage;

    private List<Collider> AlreadyCollidedWith = new List<Collider>();

     private void OnEnable()
    {
        AlreadyCollidedWith.Clear();
        //audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other==myCollider) { return; }

        if(AlreadyCollidedWith.Contains(other)) { return; }

        AlreadyCollidedWith.Add(other);
        audioSource.PlayOneShot(missSounds[Random.Range(0, missSounds.Length)]);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
            audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);

        }
        
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
    }
}
