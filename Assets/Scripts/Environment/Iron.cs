using System;
using UnityEngine;

public class Iron : Resource
{

    protected override void OnConsume(int consumedAmount)
    {
        ResourceManager.instance.iron += consumedAmount; 
        playerReference.ShowFloatingText("+" + consumedAmount + " iron", 0.5f);
        
        UIManager.instance.UpdateResourcesCount();
    }

    protected override void OnFullyConsumed()
    {
        Destroy(gameObject);
    }
}