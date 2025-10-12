using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SlimeController : Skill
{
    public DetectionController detectionController;

    private Rigidbody2D rb;
    private Animator slimeAnimator;
    private SpriteRenderer spriteRenderer;
    private Collider2D target;

    private float currentSpeed;

    private readonly float rageIn = 0;

    private Ability[] abilityList; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        slimeAnimator = GetComponent<Animator>();
        ResetIdleWalk();
        abilityList = GetComponentsInChildren<Ability>();
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

            foreach (var ability in abilityList)
            {
                ability.Use();
            }

        }
        else
        {
            IdleWalk();
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

    public override Skill GetTarget()
    {
        Skill target = null;
        _ = target.TryGetComponent<Skill>(out var skill);        
        return skill;
    }

    public override Vector2 GetTargetPosition()
    {
        return target.transform.position;
    }
}







