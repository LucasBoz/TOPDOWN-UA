using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    /**
     * Rotation
     */
    private Camera mainCamera; // Reference to the main camera
    private Vector3 mousePosition; // Position of the mouse in world coordinates
    public Vector3 rotation;
    Quaternion quaternionPosition;

    /*
     * GUN
     */
    public GameObject projectile;
    public Transform gun;         // Referência ao transform da arma
    private readonly float firehate = 3f; 
    private float cooldown = 0; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // Get the main camera
    }

    // Update is called once per frame
    void Update()
    {
        Rotation(); // Call the rotation method to update the weapon's rotation

        if (cooldown <= 0 )
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(projectile, gun.position, quaternionPosition); // Create a new projectile at the fire point
                cooldown = firehate; // Set the next fire time
            }
        }
        else
        {
            cooldown -= Time.deltaTime; // Decrease the time until the next shot
        }

    }

    private void FixedUpdate()
    {

    }

    private void Rotation()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Get the mouse position in world coordinates

        rotation = mousePosition - transform.position; // Calculate the rotation vector

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; // Calculate the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ); // Set the rotation of the weapon
        quaternionPosition = transform.rotation;
    }



}
