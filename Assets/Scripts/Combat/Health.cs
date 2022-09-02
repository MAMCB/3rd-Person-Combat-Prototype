using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 100;
    [SerializeField] bool isPlayer;
    private int health;
    public event Action OnTakeDamage;
    
    void Start()
    {
        health = MaxHealth;
    }

    public void DealDamage(int damage)
    {
        if(health==0)
        {if (!isPlayer)
            {
                Destroy(gameObject); return;
            }
         else
            {
                Debug.Log("Player is Dead");
            }
        
        }
        health = Mathf.Max(health - damage, 0);
        OnTakeDamage?.Invoke();

        if (!isPlayer)
        {
            Debug.Log("Enemy health is:" + health);
        }
        else
        {
            Debug.Log("Player health is:" + health);
        }
    }

    
}
