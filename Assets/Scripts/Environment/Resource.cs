using System;
using UnityEngine;

/*
 * Generic resource consumption manager.
 */
public abstract class Resource : MonoBehaviour
{

    public float resourceMaxAmount = 100f;
    public float resourceConsume = 10f;
    private bool isConsumable = true;

    // Consume the resource by resourceConsume amount
    public void ConsumeResource()
    {
        if (!isConsumable) return;
        
        Debug.Log("Consuming resource: " + resourceConsume + " from max: " + resourceMaxAmount);
        
        // 100 - 10 - 10 - 10
        
        if (resourceConsume > resourceMaxAmount)
        {
            OnConsume(resourceMaxAmount);
            resourceMaxAmount = 0f;
        }
        else
        {
            resourceMaxAmount -= resourceConsume;
            OnConsume(resourceConsume);
        }

        if (resourceMaxAmount == 0f)
        {
            isConsumable = false;
            OnFullyConsumed();
        }
    }

    protected abstract void OnFullyConsumed();

    protected abstract void OnConsume(float consumedAmount);

}
