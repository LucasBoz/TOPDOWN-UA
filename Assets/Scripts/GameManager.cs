using System;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Light2D globalLight;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject UIMapState;

    private int timeOfDay = 12;

    void Start()
    {
        setTimeOfDay(12);
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ToogleDayNight()
    {
        setTimeOfDay(timeOfDay == 12 ? 20 : 12);
    }

    public void setTimeOfDay(int hour)
    {
        timeOfDay = hour;
        if (hour < 6 || hour >= 18)
        {
            MakeNight();
        }
        else
        {
            MakeDay();
        }
    }

    void MakeNight()
    {
        ColorUtility.TryParseHtmlString("#01040D", out Color nightColor);
        globalLight.color = nightColor;
        player.GetComponentInChildren<Light2D>().enabled = true;
        UIMapState.GetComponent<TextMeshProUGUI>().text = "Night";
        
        player.ShowFloatingText("bit dark here, init?", 1.5f);
    }

    void MakeDay()
    {
        globalLight.color = Color.white;
        player.GetComponentInChildren<Light2D>().enabled = false;
        //UIMapState.GetComponent<TextMeshProUGUI>().text = "Day";
    }
}