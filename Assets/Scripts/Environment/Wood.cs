using System;
using UnityEngine;

public class Wood : Resource
{

    protected override void OnConsume(int consumedAmount)
    {
        ResourceManager.instance.wood += consumedAmount; 
        playerReference.ShowFloatingText("+" + consumedAmount + " wood", 0.5f);
        
        UIManager.instance.UpdateResourcesCount();
    }

    protected override void OnFullyConsumed()
    {
        Destroy(gameObject);
    }
}