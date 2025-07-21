using System;
using UnityEngine;

public class Stone : Resource
{
    
    protected override void OnConsume(int consumedAmount)
    {
        ResourceManager.instance.stone += consumedAmount; 
        playerReference.ShowFloatingText("+" + consumedAmount + " stone", 0.5f);
        
        UIManager.instance.UpdateResourcesCount();
    }

    protected override void OnFullyConsumed()
    {
        Destroy(gameObject);
    }
}