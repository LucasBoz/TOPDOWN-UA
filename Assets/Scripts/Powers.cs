using Mono.Cecil;
using UnityEngine;
using static UnityEngine.UI.Image;

public abstract class Powers : Health
{

    public float stopUntil = 0;

    private string OriginName;

    private string TargetName;

    public static readonly float speed = 2f;

    public void Rock(Attack attack, Vector3 origin, Quaternion q)
    {
        Projectile(attack, "Rock", origin, q);
    }

    public void Fireball(Attack attack, Vector3 origin, Quaternion q)
    {
        Projectile(attack, "Fireball", origin, q);
    }

    private void Projectile(Attack attack, string resource, Vector3 origin, Quaternion q)
    {
        if (Time.time >= attack.NextAttackTime)
        {
            DoAnimation(Animation.ATTACK);

            GameObject resourceGameObject = Instantiate(Resources.Load(resource, typeof(GameObject)), origin, q ) as GameObject;

            resourceGameObject.GetComponent<Projectile>().SetOriginTarget(OriginName, TargetName);

            attack.SetNextAttackTime();
        }
    }

    public void Summon(Attack attack, string resource, Vector3 origin )
    {
        if (Time.time >= attack.NextAttackTime)
        {
            DoAnimation(Animation.ATTACK);

            GameObject resourceGameObject = Instantiate(Resources.Load(resource, typeof(GameObject)), origin, Quaternion.identity) as GameObject;

            resourceGameObject.GetComponent<Powers>().SetOriginTarget(OriginName, TargetName);

            attack.SetNextAttackTime();
        }
    }


    public bool Hit(Attack attack, Collider2D target)
    {

        if (Time.time >= attack.NextAttackTime)
            return false;

        DoAnimation(Animation.ATTACK);
        target.GetComponent<Health>().TakeDamage(attack.damage);
        StopFor(attack.recoveryTime);

        attack.SetNextAttackTime();

        return true;
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


