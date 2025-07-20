using System;
using UnityEngine;

public class Tree : Resource
{
    public AudioClip chopSoundClip;
    public AudioClip treeFallSoundClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Assign a random amount of resource to the tree
        resourceMaxAmount = UnityEngine.Random.Range(50, 140);

        // Assign a random amount of resource to consume when chopping the tree
        resourceConsume = UnityEngine.Random.Range(5, 20);
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void OnConsume(float consumedAmount)
    {
        // Play the chop sound effect at the tree's position
        SoundFXManager.instance.PlaySoundFXClip(chopSoundClip, transform, 0.2f); 
        
        GetComponent<BololoAnimation>().ShakeItBololo();
        
        // Assign a random consume amount to the next chop
        resourceConsume = UnityEngine.Random.Range(5, 15);
        
        ResourceManager.instance.wood += consumedAmount; // Add the consumed amount to the wood resource
        
        playerReference.ShowFloatingText("+" + consumedAmount + " wood", 0.5f); // Show floating text with the amount of wood collected
        
        Debug.Log("TOTAL WOOD: " + ResourceManager.instance.wood);
    }

    protected override void OnFullyConsumed()
    {
        GameObject choppedTree = Instantiate(Resources.Load("ChoppedTree", typeof(GameObject))) as GameObject;
        choppedTree.transform.position = transform.position + Vector3.down * 0.982f + Vector3.right * 0.08f; // Position the chopped tree slightly below the original tree 

        Destroy(gameObject);
    }
}