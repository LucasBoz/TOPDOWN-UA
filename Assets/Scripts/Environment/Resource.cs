using System;
using UnityEngine;

/*
 * Generic resource consumption manager.
 */
public abstract class Resource : MonoBehaviour
{

    // Receives a reference to the PlayerController, so we can access player-related methods or properties if needed
    public PlayerController playerReference;
    
    public int resourceMaxAmount = 100;
    public int resourceConsume = 10;
    private bool isConsumable = true;

    public void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Consume the resource by resourceConsume amount
    public void ConsumeResource()
    {
        if (!isConsumable) return;
        
        Debug.Log("Consuming resource: " + resourceConsume + " from max: " + resourceMaxAmount);
        
        // 100 - 10 - 10 - 10
        
        if (resourceConsume > resourceMaxAmount)
        {
            OnConsume(resourceMaxAmount);
            resourceMaxAmount = 0;
        }
        else
        {
            resourceMaxAmount -= resourceConsume;
            OnConsume(resourceConsume);
        }

        if (resourceMaxAmount == 0)
        {
            isConsumable = false;
            OnFullyConsumed();
        }
    }

    protected abstract void OnFullyConsumed();

    protected abstract void OnConsume(int consumedAmount);

}
