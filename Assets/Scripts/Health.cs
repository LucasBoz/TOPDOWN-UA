using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float totalHealth = 2;


    public float currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = totalHealth;
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
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
