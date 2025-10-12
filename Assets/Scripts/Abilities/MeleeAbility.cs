using Unity.Burst.Intrinsics;
using UnityEngine;

public class MeleeAbility : Ability
{


    protected override bool Do()
    {
        var originOffset = new Vector2(0, 0.5f);
        var origin = (Vector2)transform.position + originOffset;

        var offset = 1f;
        var position = skill.GetTargetPosition();

        // Calcula a direção normalizada de origin para position
        Vector2 direction = (position - origin).normalized;


        // Calcula o target aplicando o offset na direção
        var target = origin + direction * offset;

        ShowCircleArea(target, 1f, 0.5f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(target, 1f);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<Health>().TakeDamage(100); // Deal damage to the enemy
            }

            if (hit.CompareTag("Resource"))
            {
                hit.GetComponent<Resource>().ConsumeResource();
                //if (GetComponentInChildren<PolygonCollider2D>().IsTouching(hit))
                //{
                //    var resource = hit.GetComponent<Resource>();
                //    resource.ConsumeResource();
                //}
            }
        }

        return true;
    }



}
