using System;
using UnityEngine;

public class Wood : Resource
{

    protected override void OnConsume(float consumedAmount)
    {
        ResourceManager.instance.wood += consumedAmount; 
        playerReference.ShowFloatingText("+" + consumedAmount + " wood", 0.5f);
    }

    protected override void OnFullyConsumed()
    {
        Destroy(gameObject);
    }
}