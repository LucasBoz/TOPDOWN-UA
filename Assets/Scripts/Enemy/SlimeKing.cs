using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKing : Skill
{

    public GameObject player;

    private Vector2 spawnPoint;

    private List<GameObject> children = new ();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetOriginTarget("Enemy", "Player");
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = player != null ? Vector2.Distance(player.transform.position, transform.position) : float.MaxValue;
        if (distance < 10f)
        {
            // Player is within range, perform actions
            Debug.Log("Player is within range of the Slime King.");
            // You can add more logic here, like attacking or following the player

            Summon(AttackTypes[AttackType.SUMMON], "Slime", transform.position );

        }
        else
        {
            //BackToSpawn();
            // Player is out of range, perform other actions
            Debug.Log("Player is out of range of the Slime King.");
        }
    }

    private readonly Dictionary<AttackType, Attack> AttackTypes = new()
    {
        {
            AttackType.SUMMON,
            new() {
                cooldown = 5,
            }
        },
 

    };


    public override void DoAnimation(Animation animation)
    {
        //throw new System.NotImplementedException();
    }

    protected override void Die()
    {
        //throw new System.NotImplementedException();
    }

    protected override void Hurted()
    {
        //throw new System.NotImplementedException();
    }

    public override Skill GetTarget()
    {
        throw new System.NotImplementedException();
    }

    public override Vector2 GetTargetPosition()
    {
        throw new System.NotImplementedException();
    }
}
