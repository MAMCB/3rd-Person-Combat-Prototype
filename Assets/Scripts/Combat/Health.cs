using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 100;
    private int health;
    
    void Start()
    {
        health = MaxHealth;
    }

    public void DealDamage(int damage)
    {
        if(health==0) { Destroy(gameObject); return; }
        health = Mathf.Max(health - damage, 0);
        Debug.Log(health);
    }

    
}
