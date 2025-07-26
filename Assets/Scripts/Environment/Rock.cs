using System;
using UnityEngine;

public class Rock : Resource
{
    public AudioClip pickSound;
    public float ironLotteryChance = 0.1f; // 10% chance to drop iron

    public void Start()
    {
        resourceMaxAmount = UnityEngine.Random.Range(50, 140);
        resourceConsume = UnityEngine.Random.Range(3, 9);
        base.Start();
    }
    
    protected override void OnConsume(int consumedAmount)
    {
        SoundFXManager.instance.PlaySoundFXClip(pickSound, transform, 0.5f); 
      
        GetComponent<BololoAnimation>().ShakeItBololo();
        
        ResourceManager.instance.stone += consumedAmount; 
        playerReference.ShowFloatingText("+" + consumedAmount + " stone", 0.5f);

        // might drop some iron
        if (UnityEngine.Random.value < ironLotteryChance)
        {
            int ironAmount = UnityEngine.Random.Range(1, 3); // Drop 1 to 2 iron
            ResourceManager.instance.iron += ironAmount;
            
            playerReference.ShowFloatingText("+" + ironAmount + " iron", 0.6f);
        }
        
        // Assign a random consume amount to the next chop
        resourceConsume = UnityEngine.Random.Range(3, 9);
        
        UIManager.instance.UpdateResourcesCount();
    }

    protected override void OnFullyConsumed()
    {
        Destroy(gameObject);
    }
}