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
    public event Action OnDie;
    private bool isInvulnerable;
    
    void Start()
    {
        health = MaxHealth;
    }

    public void DealDamage(int damage)
    {
        if (health == 0)
        {
            return;
        }
        if(isInvulnerable) { return; }
           
        health = Mathf.Max(health - damage, 0);
        OnTakeDamage?.Invoke();
        if (health == 0)
        {
            OnDie?.Invoke();


        }


    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    
}
