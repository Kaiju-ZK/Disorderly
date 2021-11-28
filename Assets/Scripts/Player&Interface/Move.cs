using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed = 1000f;
    [SerializeField] private float jumpForce = 2200f;
    public int StartHP;
    public static int HP = 70;
    [SerializeField] private int extraJumps = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator ani;
    private int extraJump;

    private bool isGrounded = false;
    public Transform groundCheck;
    
    public float groundRadius = 30f;
    public LayerMask whatIsGround;
    

    private void Awake()
    {
        HP = StartHP;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        extraJump = extraJumps;
    }

    private void Run()
    {
        float dir = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(dir * speed, rb.velocity.y);
        sprite.flipX = dir < 0f;
    }

    private void FixedUpdate()
    {
        HP = StartHP;
        if (Input.GetButton("Horizontal"))
            Run();
        else if (isGrounded)
          rb.velocity = new Vector2(rb.velocity.x * 0.85f, rb.velocity.y);
        ani.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x * 0.01f));
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        ani.SetBool("Ground", isGrounded);
        ani.SetFloat("FSpeed", GetComponent<Rigidbody2D>().velocity.y * 0.01f);
        if (!isGrounded)
            return;
    }

    private void Update()
    {
        if (isGrounded)
            extraJump = extraJumps;
        if (Input.GetButtonDown("Jump") && extraJump > 0)
        {
            ani.SetBool("Ground", false);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            extraJump--;
        }
        else if (Input.GetButtonDown("Jump") && extraJump == 0 && isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

}
