using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject ResourcesCount;
    
    public static UIManager instance;

    public void UpdateResourcesCount()
    {
        string content = "Wood: " + ResourceManager.instance.wood;
        content += "  Rock: " + ResourceManager.instance.stone;
        content += "  Iron: " + ResourceManager.instance.iron;
        content += "  Gold: " + ResourceManager.instance.gold;
        
        TextMeshProUGUI text = ResourcesCount.GetComponent<TextMeshProUGUI>();
        text.text = content;
    }
    
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    
}
