using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SlimeController : Health
{
    public static readonly float speed = 2f;
    public DetectionController detectionController;
    //public int health = 2;

    private Rigidbody2D rb;
    private Animator slimeAnimator;
    private SpriteRenderer spriteRenderer;
    private Collider2D target;

    private float currentSpeed;
    private float stopUntil = 0;
    public GameObject fireball;

    private readonly float rageIn = 0;
    private float timeUntilFireball = 0;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        currentSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        slimeAnimator = GetComponent<Animator>();
        ResetIdleWalk();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (stopUntil > Time.time) return;

        if (target != null)
        {
            Vector2 direction = (target.transform.position - transform.position);
            var distance = direction.magnitude;

            RunTo(direction);

            if (distance < 1)
            {
                OnAttack(AttackNames.HIT);
            }

            if (timeUntilFireball < Time.time)
            {
                OnAttack(AttackNames.FIREBOLL);
            }
        }
        else
        {
            IdleWalk();
        }

    }

    void OnAttack(AttackNames attackType)
    {
        switch (attackType)
        {
            case AttackNames.HIT: Hit(); break;
            case AttackNames.FIREBOLL: Fireball(); break;
        }
    }

  
    void Hit()
    {
        var attack = AttackTypes[AttackNames.HIT];

        if (Time.time >= attack.NextAttackTime)
        {
            slimeAnimator.SetTrigger("attack");
            target.GetComponent<PlayerController>().TakeDamage(attack.damage);
            StopFor(attack.recoveryTime);

            attack.SetNextAttackTime();
        }
    }

    void Fireball()
    {
        var attack = AttackTypes[AttackNames.HIT];

        if (Time.time >= attack.NextAttackTime)
        {
            slimeAnimator.SetTrigger("attack");

            Quaternion q = GetQuaternion(transform.position, target.transform.position);

            var x = Instantiate(fireball, transform.position, q);
            x.GetComponent<Projectile>().SetOriginTarget("Enemy", "Player");
            attack.SetNextAttackTime();
        }
    }


    private void RunTo(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction.normalized * currentSpeed * Time.deltaTime);

        spriteRenderer.flipX = direction.x < 0;
    }


    public void TargetFound(Collider2D collision)
    {
        if (target == null)
        {
            timeUntilFireball = Time.time + rageIn;
            currentSpeed = speed;
            target = collision;
            // TODO ANIMATION TARGET FOUND
            slimeAnimator.SetBool("isRunning", true);
        }
    }

    public void TargetLost(Collider2D collision)
    {
        if (target == collision)
        {
            // TODO ANIMATION TARGET LOST
            if (detectionController.detectObjs.Count > 0)
            {
                TargetFound(detectionController.detectObjs[0]);
            }
            else
            {
                target = null;
                ResetIdleWalk();
            }
        }
    }

    float timeIdleStopped;
    float timeIdleRunning;
    Vector2 directionIdle;

    private void ResetIdleWalk()
    {
        System.Random random = new();
        currentSpeed = 1;
        slimeAnimator.SetBool("isRunning", false);

        timeIdleStopped = random.Next(30) / 10;
        timeIdleRunning = random.Next(30) / 10 + timeIdleStopped;

        timeIdleStopped *= Time.time;
        timeIdleRunning *= Time.time;


        directionIdle = new Vector3(random.Next(-10, 10), random.Next(-10, 10));
    }

    private void IdleWalk()
    {
        if (timeIdleStopped > Time.time)
        {

        }
        else if (timeIdleRunning > Time.time)
        {
            slimeAnimator.SetBool("isRunning", true);
            RunTo(directionIdle);
        }
        else
        {
            ResetIdleWalk();
        }

    }



    public void StopFor(float time)
    {
        slimeAnimator.SetBool("isRunning", false);
        stopUntil = time + Time.time;
    }

    protected override void Die()
    {
        currentSpeed = 0;
        slimeAnimator.SetTrigger("morreDiabo");
    }

    protected override void Hurted()
    {
        // TODO Hurted animation
        // slimeAnimator.SetTrigger("morreDiabo");
    }

    public void Destroy()
    {
        Destroy(gameObject);

    }

  

    private readonly Dictionary<AttackNames, Attack> AttackTypes = new()
    {
        {
            AttackNames.HIT,
            new() {
                type = AttackType.MELEE,
                firehate = 3,
                damage = 1,
                castTime = 0,
                recoveryTime = 3
            }
        },
        {
            AttackNames.FIREBOLL,
            new() {
                type = AttackType.PROJECTILE,
                firehate = 5,
                damage = 3,
                castTime = 2,
                recoveryTime = 1
            }
        }

    };

    private Quaternion GetQuaternion(Vector2 from, Vector2 to)
    {
        var rotation = from - to; // Calculate the rotation vector
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; // Calculate the angle in degrees
        return Quaternion.Euler(0f, 0f, rotZ); // Set the rotation of the weapon
    }

}



public enum AttackNames
{
    HIT = 0,
    FIREBOLL = 1,
}

public class Attack
{
    public Attack()
    {
        NextAttackTime = 0;
    }

    public AttackType type;
    public float firehate;
    public int damage;
    public float castTime;
    public float recoveryTime;

    public float NextAttackTime { get; private set; }
    public void SetNextAttackTime()
    {
        NextAttackTime = Time.time + 1 / firehate;
    }

}

public enum AttackType
{
    MELEE = 0,
    PROJECTILE = 1,
}





