using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float health = 2;


    protected float currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = health;
    }


    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            Hurted();
        }
    }

    public void Heal(float x)
    {
        currentHealth += x;
    }

    protected abstract void Die(); 
    protected abstract void Hurted();



}
