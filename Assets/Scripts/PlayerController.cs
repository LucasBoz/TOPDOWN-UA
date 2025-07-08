using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidbody2D;
    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;
    public float speed = 5f;
    private float initialSpeed;
    public float runSpeed = 10f;
    private Vector2 playerDirection;
    private bool isAttack = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        if(playerDirection.sqrMagnitude > 0.1)
        {
            MovePlayer();
            playerAnimator.SetFloat("AxisX", playerDirection.x);
            playerAnimator.SetFloat("AxisY", playerDirection.y);

            playerAnimator.SetInteger("Movimento", 1);

        } else
        {
            playerAnimator.SetInteger("Movimento", 0);
        }

        if (isAttack)
        {
            playerAnimator.SetInteger("Movimento", 2);
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
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetMouseButtonDown(0))
        {
            isAttack = true;
            speed = 0f;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetMouseButtonUp(0))
        {
            isAttack = false;
            speed = initialSpeed;
        }
    }
}
