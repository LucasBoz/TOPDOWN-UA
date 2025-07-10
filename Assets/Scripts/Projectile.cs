using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Speed of the projectile

    public float lifetime = 3f; // Lifetime of the projectile in seconds
    public int damage = 1; // Damage dealt by the projectile
    public int rollPerSecond = 5; // Rotation speed of the projectile in rolls per second
    Transform sprite; // Reference to the sprite of the projectile
    private int rotationSpeed; // Speed of rotation in degrees per second

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotationSpeed = rollPerSecond * 360; // Convert rolls per second to degrees per second
        sprite = transform.GetChild(0); // Get the sprite child of the projectile
        Destroy(gameObject, lifetime); // Schedule the projectile to be destroyed after its lifetime
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the projectile around its Z-axis
        sprite.Rotate(0, 0, rotationSpeed * Time.deltaTime); // Rotate the sprite
        transform.Translate(Vector3.right * speed * Time.deltaTime); // Move the projectile forward
    }

    void DestroyProjectile()
    {
        Destroy(gameObject); // Destroy the projectile game object
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && !collision.CompareTag("Player"))
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<SlimeController>().TakeDammage(damage); // Deal damage to the enemy's health
            }
            DestroyProjectile();
        }
    }

}
