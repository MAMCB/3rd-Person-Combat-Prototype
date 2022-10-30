using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] AudioClip[] missSounds;
    [SerializeField] AudioSource audioSource;
    [SerializeField] EnemyStateMachine enemyStateMachine;
    [SerializeField] bool isEnemy = false;
    [SerializeField] BloodDecalManager bodyBloodManager;
    private int damage;
    private float knockback;

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
            if(isEnemy && other.CompareTag("Enemy")) { return; }
            
            health.DealDamage(damage);
            audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
            health.ActivateBloodEffect();
            if(!isEnemy)
            {
                bodyBloodManager.IncreaseBloodStains();
            }
            
        }
        else
        {
            if(isEnemy)
            {
                enemyStateMachine.AttackMissed = true;
            }
            
        }
        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))

        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }

        
    }

   

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
