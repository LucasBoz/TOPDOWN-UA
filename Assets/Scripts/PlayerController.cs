using System;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerController : Powers
{
    private Rigidbody2D playerRigidbody2D;
    private Animator playerAnimator;
    public float speed = 5f;
    private float initialSpeed;
    public float runSpeed = 10f;
    private Vector2 playerDirection;
    public float attackSpeed = 1f;



    /*
    * GUNS
    */
    public Aim aim; // Reference to the Weapon script
    private float nextAttackTime = 0;
    private readonly float firehate = .5f;
    public GameObject rock;
    public GameObject fireball;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = 100;
        playerAnimator = GetComponent<Animator>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
        SetOriginTarget("Player", "Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRun();
        OnAttack();
    }

    void FixedUpdate()
    {
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //playerAnimator.SetInteger("Movimento", playerDirection.sqrMagnitude > 0.1 ? 1 : 0);

        if (playerDirection.sqrMagnitude > 0.1)
        {
            MovePlayer();
            playerAnimator.SetFloat("AxisX", playerDirection.x);
            playerAnimator.SetFloat("AxisY", playerDirection.y);

            playerAnimator.SetInteger("Movimento", 1);
        }
        else
        {
            playerAnimator.SetInteger("Movimento", 0);
        }

    }

    void MovePlayer()
    {
        playerRigidbody2D.MovePosition(playerRigidbody2D.position + playerDirection.normalized * speed * Time.deltaTime);

    }


    void PlayerRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = runSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            speed = initialSpeed;
        }
    }

    void OnAttack()
    {
        switch (Input.inputString)
        {
            case "1": Sword(); break;
            case "2": Rock(AttackTypes[AttackType.ROCK], aim.transform.position, aim.quaternionPosition); break;
            case "3": Fireball(AttackTypes[AttackType.FIREBOLL], aim.transform.position, aim.quaternionPosition); break;
        }
    }

    void Sword()
    {
        if (Time.time >= nextAttackTime)
        {
            playerAnimator.SetTrigger("Attack");
            Collider2D[] hits = Physics2D.OverlapCircleAll(aim.transform.position, 1f);

            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    hit.GetComponent<Health>().TakeDamage(1); // Deal damage to the enemy
                }

                if (hit.CompareTag("Resource"))
                {
                    if (GetComponentInChildren<PolygonCollider2D>().IsTouching(hit))
                    {
                        var resource = hit.GetComponent<Resource>();
                        resource?.ConsumeResource();
                    }
                }
            }


            nextAttackTime = Time.time + attackSpeed / firehate; // Set the next fire time
        }
    }

   
    protected override void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        playerAnimator.SetTrigger("Die");
        this.enabled = false;
    }

    protected override void Hurted()
    {
        playerAnimator.SetTrigger("Hurt");
    }

    public override void DoAnimation(Animation animation)
    {
        switch (animation)
        {
            case Animation.ATTACK:
                playerAnimator.SetTrigger("attack");
                break;
            case Animation.IDLE:
                playerAnimator.SetTrigger("idle");
                break;
            case Animation.WALK:
                playerAnimator.SetBool("isRunning", false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(animation), animation, null);
        }
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
}
