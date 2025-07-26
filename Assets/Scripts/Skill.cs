using Mono.Cecil;
using System;
using UnityEngine;
using static UnityEngine.UI.Image;

public abstract class Skill : Health
{

    public float stopUntil = 0;

    public string OriginName;

    private string TargetName;


    public static readonly float speed = 2f;

    public abstract Skill GetTarget();



    public abstract Vector2 getTargetPosition();




    public void Summon(Attack attack, string resource, Vector3 origin )
    {
        if (Time.time >= attack.NextAttackTime)
        {
            DoAnimation(Animation.ATTACK);

            UnityEngine.GameObject resourceGameObject = Instantiate(Resources.Load(resource, typeof(UnityEngine.GameObject)), origin, Quaternion.identity) as UnityEngine.GameObject;

            resourceGameObject.GetComponent<Skill>().SetOriginTarget(OriginName, TargetName);

            attack.SetNextAttackTime();
        }
    }



    public void StopFor(float time)
    {
        DoAnimation(Animation.IDLE);
        stopUntil = time + Time.time;
    }

    public void SetOriginTarget(string origin, string target)
    {
        OriginName = origin;
        TargetName = target;
    }


    public abstract void DoAnimation(Animation animation);

    public virtual Vector2 GetOffSet()
    {
        return Vector2.zero;  
    }

}


public enum Animation
{
    IDLE = 0,
    WALK = 1,
    RUN = 2,
    ATTACK = 3,
    HURT = 4,
    DIE = 5
}

public enum AttackType
{
    HIT = 0,
    FIREBOLL = 1,
    ROCK = 2,
    SUMMON = 3,
}



public class Attack
{
    public Attack()
    {
        NextAttackTime = 0;
    }

    public float cooldown;
    public int damage;
    public float castTime;
    public float recoveryTime;

    public float NextAttackTime { get; private set; }

    // Cooldown time for the next attack
    public void SetNextAttackTime()
    {
        NextAttackTime = Time.time + cooldown;
    }

 

}


