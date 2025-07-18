using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // TODO - Using fixed "resource" types for now, but this can be some sort onf inventory system in the future

    public float wood = 0f;
    // public float rock = 0f;
    // public float water = 0f;
    // public float sand = 0f;
    // public float glass = 0f;
    // ... add more resources as needed

    public static ResourceManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

}