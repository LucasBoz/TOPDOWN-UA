using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // TODO - Using fixed "resource" types for now, but this can be some sort onf inventory system in the future

    public int wood = 0;
    public int stone = 0;
    public int iron = 0;
    public int gold = 0;

    public static ResourceManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
            instance = this;
        
        UIManager.instance.UpdateResourcesCount();
    }

}