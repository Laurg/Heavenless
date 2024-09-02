using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Character_Stats
{
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private bool canAttack;
    [SerializeField] public GameObject EnemyType;
    [SerializeField] public GameObject Boss;

    private void Start()
    {
        InitVariables();
    }

    public void DealDamage()
    {
        // Damaging functionality
    }

    public void OnCollisionEnter(Collision collision)
    {
        health -= 10;
        SetHealthTo(health);
    }
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    public override void InitVariables()
    {
        
        if (EnemyType == Boss)
        {
            maxHealth = 500;
            SetHealthTo(maxHealth);
            isDead = false;

            damage = 10;
            attackSpeed = 1.5f;
            canAttack = true;
        }
        else
        {
            maxHealth = 30;
            SetHealthTo(maxHealth);
            isDead = false;

            damage = 10;
            attackSpeed = 1.5f;
            canAttack = true;
        }
    }

}
