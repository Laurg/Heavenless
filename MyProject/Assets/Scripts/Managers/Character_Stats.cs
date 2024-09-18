using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_Stats : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected bool isDead;

    public Healthbar healthbar;
    private void Start()
    {

        InitVariables();
        health = maxHealth;
        healthbar.SetMaxHealth (health);
    }

    public virtual void CheckHealth()
    {
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public virtual void Die()
    {
        isDead = true;
        SceneManager.LoadScene(2);
    }

    public bool IsDead()
    {

        return isDead;
    }  
    public void SetHealthTo(int healthToSetTo)
    {
        health = healthToSetTo;
        CheckHealth();
    }

    public void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Enemy"))
       // {
            TakeDamage(damage);
        //}
    }

    // Método para recibir daño
    public void TakeDamage(int amount)
    {
        int healthAfterDamage = health - amount;
        SetHealthTo(healthAfterDamage);
        healthbar.SetHealth(health); 
    }

    public void Heal(int heal)
    {
        int healthAfterHeal = health + heal;
        SetHealthTo(healthAfterHeal);
    }

    public virtual void InitVariables()
    {
        maxHealth = 100;
        SetHealthTo(maxHealth);
        isDead = false;
    }
}
