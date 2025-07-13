using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SlimeController : Powers
{
    public DetectionController detectionController;

    private Rigidbody2D rb;
    private Animator slimeAnimator;
    private SpriteRenderer spriteRenderer;
    private Collider2D target;

    private float currentSpeed;
    public GameObject fireball;

    private float timeUntilFireball = 0;
    private readonly float rageIn = 0;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        slimeAnimator = GetComponent<Animator>();
        ResetIdleWalk();
        SetOriginTarget("Enemy", "Player");
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
                OnAttack(AttackType.HIT);
            }

            OnAttack(AttackType.FIREBOLL);
            OnAttack(AttackType.ROCK);


        }
        else
        {
            IdleWalk();
        }

    }

    void OnAttack(AttackType attackType)
    {
        var attack = AttackTypes[attackType];

        var q = GetQuaternion(transform.position, target.transform.position );

        switch (attackType)
        {
            case AttackType.HIT: Hit(attack, target); break;
            case AttackType.FIREBOLL: Fireball(attack, transform.position, q); break;
            case AttackType.ROCK: Rock(attack, transform.position, q); break;
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



    protected override void Die()
    {
        currentSpeed = 0;
        slimeAnimator.SetTrigger("morreDiabo");
    }

    protected override void Hurted()
    {
        // TODO Hurted animation
        // slimeAnimator.SetTrigger("hurt");
    }

    public void Destroy()
    {
        Destroy(gameObject);

    }

  

    private readonly Dictionary<AttackType, Attack> AttackTypes = new()
    {
        {
            AttackType.HIT,
            new() {
                cooldown = 3,
                damage = 1,
                castTime = 0,
                recoveryTime = 3
            }
        },
        {
            AttackType.FIREBOLL,
            new() {
                cooldown = 5,
                damage = 3,
                castTime = 2,
                recoveryTime = 2
            }
        },
        {
            AttackType.ROCK,
            new() {
                cooldown = 1,
                damage = 1,
                castTime = 2,
                recoveryTime = 2
            }
        }

    };


    public static Quaternion GetQuaternion(Vector2 from, Vector2 to)
    {
        var rotation = to - from; // Calculate the rotation vector
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; // Calculate the angle in degrees
        return Quaternion.Euler(0f, 0f, rotZ); // Set the rotation of the weapon
    }


    public override void DoAnimation(Animation animation)
    {
        switch (animation)
        {
            case Animation.ATTACK:
                slimeAnimator.SetTrigger("attack");
                break;
            case Animation.IDLE:
                slimeAnimator.SetTrigger("idle");
                break;
            case Animation.WALK:
                slimeAnimator.SetBool("isRunning", false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(animation), animation, null);
        }
    }
}







