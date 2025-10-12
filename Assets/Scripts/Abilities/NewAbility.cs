using System.Linq;
using UnityEditor.Playables;
using UnityEngine;

public class NewAbility : MonoBehaviour
{

    public GameObject ability;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ability.GetComponent<Ability>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();

        if (player)
        {
            player.AddAbility(ability);
            Destroy(gameObject);
        }
    }
}
