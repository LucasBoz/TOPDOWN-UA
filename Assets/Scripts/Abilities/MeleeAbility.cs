using Unity.Burst.Intrinsics;
using UnityEngine;

public class MeleeAbility : Ability
{


    protected override bool Do()
    {
        //playerAnimator.SetTrigger("Attack") ; TODO
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<Health>().TakeDamage(1); // Deal damage to the enemy
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
