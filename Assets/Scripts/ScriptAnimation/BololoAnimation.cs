using UnityEngine;

/*
 * An animation that does Sprite shakes It.... Shake it Bololo, ha ha. bololo, hahaha!
 */
public class BololoAnimation : MonoBehaviour
{
    
    public float duration = 0.2f;
    public float magnitude = 0.1f;
    public float delay = 0f;
    
    public void ShakeItBololo()
    {
        StartCoroutine(DoAnimation());
    }
    
    private System.Collections.IEnumerator DoAnimation()
    {
        if (delay > 0f)
        {
            yield return new WaitForSeconds(delay);
        }
        
        Vector3 pos = transform.localPosition;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(pos.x + x, pos.y + y, pos.z);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = pos;
    }
    
}
