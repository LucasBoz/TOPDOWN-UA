using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Health
{
    private Rigidbody2D playerRigidbody2D;
    private Animator playerAnimator;
    public float speed = 5f;
    private float initialSpeed;
    public float runSpeed = 10f;
    private Vector2 playerDirection;


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
        playerAnimator = GetComponent<Animator>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
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
            case "2": Rock(); break;
            case "3": Fireball(); break;
        }

    }

    void Sword()
    {
        if (Time.time >= nextAttackTime)
        {
            playerAnimator.SetTrigger("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(aim.transform.position, 1f);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<SlimeController>().TakeDamage(1); // Deal damage to the enemy
                }
            }


            nextAttackTime = Time.time + 1f / firehate; // Set the next fire time
        }
    }

    void Rock()
    {
        if (Time.time >= nextAttackTime)
        {
            playerAnimator.SetTrigger("Attack");
            Shoot(rock);
            nextAttackTime = Time.time + 1f / firehate; // Set the next fire time
        }
    }

    void Fireball()
    {
        if (Time.time >= nextAttackTime)
        {
            playerAnimator.SetTrigger("Attack");
            Shoot(fireball);
            nextAttackTime = Time.time + 1f / firehate; // Set the next fire time
        }
    }

    public void Shoot(GameObject projectile)
    {
        Instantiate(projectile, aim.transform.position, aim.quaternionPosition );
    }


    public void OnEndAction()
    {

    }



    private void Hurt()
    {
        // TODO DEBUFF 
        playerAnimator.SetTrigger("Hurt");
    }

    protected override void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        playerAnimator.SetTrigger("Die");
        this.enabled = false;
    }

    protected override void Hurted()
    {
        throw new System.NotImplementedException();
    }
}
