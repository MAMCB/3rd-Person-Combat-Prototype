using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 100;
    [SerializeField] bool isPlayer;
    private int health;
    
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
