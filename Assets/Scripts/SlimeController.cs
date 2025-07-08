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
      
        if (detectionController?.detectObjs.Count > 0)
        {
            slimeAnimator.SetBool("isRunning", true); 

            direction = (detectionController.detectObjs[0].transform.position - transform.position).normalized;

            spriteRenderer.flipX = direction.x < 0;

            rb.MovePosition(rb.position + direction * speed * Time.deltaTime  );

            spriteRenderer.flipX = direction.x < 0;

        } else
        {
            slimeAnimator.SetBool("isRunning", false);
            IdleWalk();

        }

        if (health <= 0)
        {
            Die();
        }


    }

    private void IdleWalk()
    {
        
    }

    public void TakeDammage(int damage)
    {
        health -= damage;
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
