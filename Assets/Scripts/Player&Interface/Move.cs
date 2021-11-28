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
    private float fallTimer = 0.2f;

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

    private void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            extraJump = extraJumps;
            isGrounded = true;
            fallTimer = 0.25f;
        }
        else
        {
            isGrounded = false;
            fallTimer -= Time.deltaTime;
        }
        if (rb.velocity.y > 0)
            ani.Play("SitAni");
        else if (rb.velocity.y < 0 && fallTimer < 0)
            ani.Play("FalAni");
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (isGrounded && rb.velocity.y == 0)
                ani.Play("RunAni");
            sprite.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (isGrounded && rb.velocity.y == 0)
                ani.Play("RunAni");
            sprite.flipX = true;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (isGrounded && rb.velocity.y == 0)
                ani.Play("StayAni");
        }
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && extraJump > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            ani.Play("SitAni");
            extraJump--;
        }
        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && extraJump == 0 && isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            ani.Play("SitAni");
            extraJump--;
        }            
    }
}
