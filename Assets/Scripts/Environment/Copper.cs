using System;
using UnityEngine;

public class Copper : Resource
{

    protected override void OnConsume(int consumedAmount)
    {
        ResourceManager.instance.copper += consumedAmount; 
        playerReference.ShowFloatingText("+" + consumedAmount + " copper", 0.5f);
        
        UIManager.instance.UpdateResourcesCount();
    }

    protected override void OnFullyConsumed()
    {
        Destroy(gameObject);
    }
}