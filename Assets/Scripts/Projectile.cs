using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Speed of the projectile

    public float lifetime = 3f; // Lifetime of the projectile in seconds
    public int damage = 1; // Damage dealt by the projectile
    public float distance = 1;
    public LayerMask whatIsSolid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(DestroyProjectile), lifetime); // Schedule the projectile to be destroyed after its lifetime
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime); // Move the projectile forward

    }

    void DestroyProjectile()
    {
        Destroy(gameObject); // Destroy the projectile game object
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<SlimeController>().TakeDammage(damage); // Deal damage to the enemy's health
            }
            DestroyProjectile();
        }
    }

}
