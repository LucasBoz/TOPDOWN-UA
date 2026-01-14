using System;
using System.Collections.Generic;
using System.Linq;
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

    private GameObject abilities;

    /*
    * GUNS
    */
    public List<Ability> abilityList;

    public Aim aim; // Reference to the Weapon script


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abilityList = GetComponentsInChildren<Ability>().ToList();

        playerAnimator = GetComponent<Animator>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        abilities = GameObject.Find("Abilities");
        initialSpeed = speed;
        SetOriginTarget("Player", "Enemy");
        UIManager.instance.Init();
    }



    public void AddAbility(GameObject ability)
    {
        var x = Instantiate(ability);
        x.transform.position = Vector3.zero;
        x.transform.SetParent(abilities.transform, false);
        abilityList = GetComponentsInChildren<Ability>().ToList();
        UIManager.instance.UpdateAbilityFrames();
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

        if (index > -1 && index < abilityList.Count)
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

    public override Vector2 GetTargetPosition()
    {
        return aim.mousePosition;
    }

    public override Vector2 GetOffSet()
    {
        return new(0, 0.5f); ;
    }


}
