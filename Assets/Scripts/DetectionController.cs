using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
    public string targetTag = "Player";

    public SlimeController SlimeController;

    public List<Collider2D> detectObjs = new();

    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            detectObjs.Add(collision);
            Debug.Log($"Object detected: {collision.name}");
            // Notify the SlimeController about the detected object
            SlimeController.TargetFound(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            detectObjs.Remove(collision);
            SlimeController.TargetLost(collision);
            Debug.Log($"Object exited: {collision.name}");
        }
    }
}


