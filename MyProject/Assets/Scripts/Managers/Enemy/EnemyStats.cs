using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Character_Stats
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private bool canAttack;
    [SerializeField] public GameObject EnemyType;
    [SerializeField] public GameObject Boss;
    public string tagToIgnore = "enemyBullet";

    private void Start()
    {
        InitVariables();
        healthbar.SetHealth(health);
    }

    public void DealDamage()
    {
        // Damaging functionality
    }

    public override void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

    public override void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag(tagToIgnore))
        {
            Debug.Log("Colisión ignorada con: " + collision.gameObject.name);
            return;
        }
        else { 
        base.OnCollisionEnter(collision);
        }
    }
    public override void InitVariables()
    {
        
        if (EnemyType == Boss)
        {
            maxHealth = 500;
            SetHealthTo(maxHealth);
            isDead = false;

            damage = 20;
            attackSpeed = 1.5f;
            canAttack = true;
        }
        else
        {
            maxHealth = 50;
            SetHealthTo(maxHealth);
            isDead = false;

            damage = 10;
            attackSpeed = 1.5f;
            canAttack = true;
        }
    }

}
