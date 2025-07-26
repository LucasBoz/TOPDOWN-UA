using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    public string abilityName;
    public Sprite sprite;

    // dont  show atribute on UI
    [HideInInspector]
    public Image imageIcon; // Reference to the UI Image component for displaying the ability icon

    [HideInInspector]
    public Image cooldownImage;

    /*    
     * Atributes
     */
    public float cooldown = 2;
    private float nextAttackTime = 0;

    public Skill skill; 

    public void Start()
    {
        skill = GetComponentInParent<Skill>();
    }

    public void UpdateCooldownImage()
    {
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = GetCooldownPercent();
        }
    }

    public float GetCooldownPercent()
    {
        // Calculate the percentage of cooldown remaining
        if (cooldown <= 0)
        {
            return 0f; // No cooldown
        }
        float remaining = nextAttackTime - Time.time;

        if (remaining <= 0)
        {
            return 0; // Still in cooldown
        }

        return remaining / cooldown;
    }

    public void SetNextAttackTime()
    {
        nextAttackTime = Time.time + cooldown;
    }

    public bool IsReady()
    {
        return Time.time >= nextAttackTime;
    }


    public bool Use()
    {
        if (!IsReady())
        {
            return false; // Ability is on cooldown
        }
        bool result = Do();
        if (result)
        {
            SetNextAttackTime(); // Reset the cooldown
        }
        return result;
    }

    // Called when someone tries to use the ability
    protected abstract bool Do();




}
