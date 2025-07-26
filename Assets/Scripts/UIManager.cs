using Mono.Cecil;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI ironText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Image life;

    public PlayerController playerController;

    public Abilities abilities;

    public GameObject abilityFrame;
    public GameObject abilityUI;

    private void FixedUpdate()
    {
        life.fillAmount = playerController.currentHealth / playerController.totalHealth;

        UpdateCooldown();

    }

    void Start()
    {
        if (instance == null)
            instance = this;

    }

    public void UpdateResourcesCount()
    {
        woodText.text = ResourceManager.instance.wood.ToString();
        stoneText.text = ResourceManager.instance.stone.ToString();
        ironText.text = ResourceManager.instance.iron.ToString();
        goldText.text = ResourceManager.instance.gold.ToString();

    }

    public void Init()
    {
        InstantiateAbilityFrames();
        UpdateResourcesCount();
    }

    private void InstantiateAbilityFrames()
    {
        if (playerController.abilityList == null) return;

        // left of the position of the abilityUI
        Vector2 startPosition = new(-200, -10);

        foreach (var ability in playerController.abilityList)
        {
            if (ability == null) continue;
            // Check if the ability has a sprite
            if (ability.sprite == null)
            {
                Debug.LogWarning($"Ability {ability.name} does not have a sprite assigned.");
                continue;
            }

            GameObject frame = Instantiate(abilityFrame, startPosition, Quaternion.identity);
            frame.transform.SetParent(abilityUI.transform, false);

            Image[] imageList = frame.GetComponentsInChildren<Image>();

            Image image = imageList.Where(e => e.name == "Image").FirstOrDefault();
            image.sprite = ability.sprite;

            ability.imageIcon = image;
            ability.cooldownImage = imageList.Where(e => e.name == "Cooldown").First();


            startPosition.x += 60; // Move to the right for the next frame
        }

    }

    private void UpdateCooldown()
    {
        if (playerController.abilityList == null) return;

        foreach (var ability in playerController.abilityList)
        {
            ability.UpdateCooldownImage();
        }

    }


}
