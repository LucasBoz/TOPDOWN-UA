using System;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerController : Skill
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
    public Ability[] abilityList ;

    public Aim aim; // Reference to the Weapon script
    private float nextAttackTime = 0;
    private readonly float firehate = .5f;
    public GameObject rock;
    public GameObject fireball;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abilityList = GetComponentsInChildren<Ability>();
       
        playerAnimator = GetComponent<Animator>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        initialSpeed = speed;
        SetOriginTarget("Player", "Enemy");
        UIManager.instance.Init();
    }

 

    // Update is called once per frame
    void Update()
    {
        PlayerRun();
        OnAttack();
    }

    public void ShowFloatingText(string text, float duration = 0.5f)
    {
        // instantiate a floating text prefab
        // the FloatingText script will take care of animating and destroying the text after the duration

        GameObject floatingTextPrefab = Resources.Load("FloatingText") as GameObject;
        if (floatingTextPrefab)
        {
            GameObject floatingObject = Instantiate(floatingTextPrefab, transform);

            FloatingText floatingText = floatingObject.GetComponent<FloatingText>();

            floatingText.text = text;
            floatingText.duration = duration;
            floatingText.offset = 1.2f;
        }
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

        var index = GetAbilityIndex();

        if(index > -1 && index < abilityList.Length)
        {
            if (abilityList[index].Use()) playerAnimator.SetTrigger("Attack");
        }

    }


    int GetAbilityIndex()
    {
        return Input.inputString switch
        {
            "j" => 0,
            "k" => 1,
            "l" => 2,
            "u" => 3,
            "i" => 4,
            "o" => 5,
            "p" => 6,
            _ => int.TryParse(Input.inputString, out int index) ? index - 1 : -1
        };

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
                        resource.playerReference = this;
                        resource.ConsumeResource();
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

    public override Skill GetTarget()
    {
        throw new NotImplementedException();
    }

    public override Vector2 getTargetPosition()
    {
        return aim.mousePosition;
    }

    public override Vector2 GetOffSet()
    {
        return new(0, 0.5f); ;
    }

  
}
