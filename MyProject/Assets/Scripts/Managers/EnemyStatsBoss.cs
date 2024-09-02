using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsBoss : Character_Stats
{
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private bool canAttack;

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
        maxHealth = 200;
        SetHealthTo(maxHealth);
        isDead = false;

        damage = 10;
        attackSpeed = 1.5f;
        canAttack = true;
    }

}