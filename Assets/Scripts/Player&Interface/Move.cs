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
    private bool isAttacking = false;
    [SerializeField] private Transform groundCheck;    
    [SerializeField] private GameObject attackHitBox;    

    private void Awake()
    {
        HP = StartHP;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        extraJump = extraJumps;
        attackHitBox.SetActive(false);
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
        if (Input.GetKey(KeyCode.D) && !isAttacking)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (isGrounded && rb.velocity.y == 0)
                ani.Play("RunAni");
            transform.localScale = new Vector2(600, 600);
        }
        else if (Input.GetKey(KeyCode.A) && !isAttacking)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (isGrounded && rb.velocity.y == 0)
                ani.Play("RunAni");
            transform.localScale = new Vector2(-600, 600);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (isGrounded && rb.velocity.y == 0)
                if (!isAttacking)
                    ani.Play("StayAni");
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking && isGrounded)
        {
            isAttacking = true;
            int Choose = UnityEngine.Random.Range(1, 3);
            ani.Play("Attack" + Choose);
            StartCoroutine(DoAttack());
        }
        
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


    IEnumerator DoAttack()
    {
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attackHitBox.SetActive(false);

        isAttacking = false;
    }
}
