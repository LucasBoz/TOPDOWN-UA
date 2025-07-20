using System.Collections;
using TMPro;
using UnityEngine;

/*
 * TODO - Two or more floating texts should not overlap. May be good to use a queue to manage them.
 */
public class FloatingText : MonoBehaviour
{
    public string text;
    public float duration = 0.5f; // Duration for which the text will be visible
    public float offset = 0f; // Start Y offset for the text
    private TextMeshProUGUI textUI;
    private bool isFading = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textUI = GetComponentInChildren<TextMeshProUGUI>();
        textUI.text = text;
        textUI.transform.position = transform.position + new Vector3(0, offset, 0);
    }

    // Update is called once per frame
    void Update()
    {
        textUI.transform.position = textUI.transform.position + Vector3.up * 0.001f;
        textUI.alpha = Mathf.Lerp(textUI.alpha, 0, Time.deltaTime / (duration));
        
        Destroy(gameObject, duration + 0.3f);
    }
    
}