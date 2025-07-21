using System;
using UnityEngine;

public class Rock : Resource
{
    public AudioClip pickSound;
    
    void Start()
    {
        resourceMaxAmount = UnityEngine.Random.Range(50, 140);
        resourceConsume = UnityEngine.Random.Range(3, 9);
    }
    
    protected override void OnConsume(int consumedAmount)
    {
        SoundFXManager.instance.PlaySoundFXClip(pickSound, transform, 0.5f); 
      
        GetComponent<BololoAnimation>().ShakeItBololo();
        
        ResourceManager.instance.stone += consumedAmount; 
        playerReference.ShowFloatingText("+" + consumedAmount + " stone", 0.5f);
        
        // Assign a random consume amount to the next chop
        resourceConsume = UnityEngine.Random.Range(3, 9);
        
        UIManager.instance.UpdateResourcesCount();
    }

    protected override void OnFullyConsumed()
    {
        Destroy(gameObject);
    }
}