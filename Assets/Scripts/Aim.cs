using System;
using UnityEngine;

public class Aim : MonoBehaviour
{
    /**
     * Rotation
     */
    private Camera mainCamera; // Reference to the main camera
    public Vector3 mousePosition; // Position of the mouse in world coordinates
    private Vector3 rotation;
    public Quaternion quaternionPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // Get the main camera
    }

    // Update is called once per frame
    void Update()
    {
        Rotation(); // Call the rotation method to update the weapon's rotation
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
