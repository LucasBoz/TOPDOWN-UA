using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float speed = 10f; // Speed of the projectile

    public float lifetime = 3f; // Lifetime of the projectile in seconds
    public int damage = 1; // Damage dealt by the projectile

    public string origin = "Player";
    public string target = "Enemy";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime); // Schedule the projectile to be destroyed after its lifetime
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the projectile around its Z-axis
        transform.Translate(Vector3.right * speed * Time.deltaTime); // Move the projectile forward
    }

    void DestroyProjectile()
    {
        Destroy(gameObject); // Destroy the projectile game object
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && !collision.CompareTag(origin))
        {
            if (collision.gameObject.TryGetComponent<Health>(out var target)) { 
                target.TakeDamage(damage); // Deal damage to the enemy's health
            }

            DestroyProjectile();
        }
    }

    public void SetTarget(string t)
    {
        target = t;
    }

    public void SetOrigin(string o)
    {
        origin = o;
    }
}
