using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
    public string targetTag = "Player";

    public List<Collider2D> detectObjs = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            detectObjs.Add(collision);
            Debug.Log($"Object detected: {collision.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            detectObjs.Remove(collision);
            Debug.Log($"Object exited: {collision.name}");
        }
    }
}


