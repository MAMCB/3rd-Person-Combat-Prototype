using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 100;
    [SerializeField] bool isPlayer;
    [SerializeField] HealthBar healthBar;
    private int health;
    public event Action OnTakeDamage;
    public event Action OnDie;
    private bool isInvulnerable;
    public bool Isdead => health==0;

    void Start()
    {
        health = MaxHealth;
        if (isPlayer) { healthBar.SetMaxHealth(MaxHealth); }
        
    }

    private void Update()
    {
        if (isPlayer)
        {
            healthBar.SetHealth(health);
        }
        
        
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
