using System;
using UnityEngine;

public class Gold : Resource
{

    protected override void OnConsume(int consumedAmount)
    {
        ResourceManager.instance.gold += consumedAmount; 
        playerReference.ShowFloatingText("+" + consumedAmount + " gold", 0.5f);
        
        UIManager.instance.UpdateResourcesCount();
    }

    protected override void OnFullyConsumed()
    {
        Destroy(gameObject);
    }
}