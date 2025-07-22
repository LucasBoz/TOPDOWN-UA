using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI ironText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Image life;

    public Health health;

    private void FixedUpdate()
    {
        life.fillAmount = health.currentHealth / health.totalHealth;
    }

    void Start()
    {
        if (instance == null)
            instance = this;
        UpdateResourcesCount();
    }

    public void UpdateResourcesCount()
    {
        woodText.text = ResourceManager.instance.wood.ToString();
        stoneText.text = ResourceManager.instance.stone.ToString();
        ironText.text = ResourceManager.instance.iron.ToString();
        goldText.text = ResourceManager.instance.gold.ToString();
    }



}
