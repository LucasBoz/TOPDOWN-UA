using System.Collections;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{

    [Range(0,1)]
    [SerializeField] private float transparencyValue = 0.7f;
    [Range(0, 1)]
    [SerializeField] private float transparencyFadeTime = 0.5f;

    [SerializeField] private GameObject objectToFade;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = objectToFade.GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            StartCoroutine(Fade(spriteRenderer, transparencyFadeTime, spriteRenderer.color.a, transparencyValue));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            StartCoroutine(Fade(spriteRenderer, transparencyFadeTime, spriteRenderer.color.a, 1f));
        }
    }

    private IEnumerator Fade(SpriteRenderer spriteTransparency, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newTransparency = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteTransparency.color = new Color(spriteTransparency.color.r, spriteTransparency.color.g, spriteTransparency.color.b, newTransparency);
            yield return null;
        }
    }

}
