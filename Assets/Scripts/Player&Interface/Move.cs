using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ani;

    [SerializeField] private float speed = 1000f;
    [SerializeField] private float jumpForce = 2200f;
    public int HP = 100;

    private float fallTimer = 0.2f;
    public float hitTimer;
    private int extraJump;
    private bool isGrounded = false;
    [SerializeField] private int extraJumps = 1;
    [SerializeField] private Transform groundCheck;    

    private bool isAttacking = false;
    [SerializeField] private Transform attackHitBox;    
    [SerializeField] private LayerMask enemy;
    public float attackRadius = 20f;
    public int Damage = 30;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        extraJump = extraJumps;
    }

    private void FixedUpdate()
    {
        if (hitTimer > 0)
            hitTimer -= Time.deltaTime;
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
        if (rb.velocity.y > 0 && fallTimer < 0 && hitTimer <= 0)
            ani.Play("SitAni");
        else if (rb.velocity.y < 0 && fallTimer < 0 && hitTimer <= 0)
            ani.Play("FalAni");
        if (Input.GetKey(KeyCode.D) && !isAttacking && hitTimer <= 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (isGrounded && rb.velocity.y == 0)
                ani.Play("RunAni");
            transform.localScale = new Vector2(600, 600);
        }
        else if (Input.GetKey(KeyCode.A) && !isAttacking && hitTimer <= 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (isGrounded && rb.velocity.y == 0)
                ani.Play("RunAni");
            transform.localScale = new Vector2(-600, 600);
        }
        else
        {
            if (hitTimer <= 0)
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
        if (HP < 35)
        {
            if (Time.timeScale == 1)
                gameObject.GetComponent<AudioSource>().Play();
            HP = 0;
            Time.timeScale = 0;
            ani.Play("SitAni");
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }


    IEnumerator DoAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < enemies.Length; i++) 
        {
            enemies[i].GetComponent<EnemyMove>().HP -= Damage;
            enemies[i].GetComponent<EnemyMove>().hitTimer = 0.5f;
            if (transform.localScale.x > 0)
            {
                enemies[i].GetComponent<Rigidbody2D>().velocity = new Vector2(700f, 1000f);
            }
            else
            {
                enemies[i].GetComponent<Rigidbody2D>().velocity = new Vector2(-700f, 1000f);
            }
        }
        yield return new WaitForSeconds(0.5f);


        isAttacking = false;
    }
}
