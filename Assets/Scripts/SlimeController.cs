using System;
using UnityEngine;

public class SlimeController : MonoBehaviour
{

    public float speed = 2f;
    private Vector2 direction;
    private Rigidbody2D rb;
    private Animator slimeAnimator;
    public DetectionController detectionController;
    public int health = 2;
    private Collider2D target;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        slimeAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            direction = (target.transform.position - transform.position).normalized;

            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

            spriteRenderer.flipX = direction.x < 0;
        }

    }


    public void TargetFound(Collider2D collision)
    {
        if (target == null)
        {
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
                slimeAnimator.SetBool("isRunning", false);
                IdleWalk();
            }
        }
    }

    private void IdleWalk()
    {

    }

    public void TakeDammage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        speed = 0;
        slimeAnimator.SetTrigger("morreDiabo");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }




}
