using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ani;

    [SerializeField] private float speed = 500f;
    //[SerializeField] private float jumpForce = 2200f;
    public int HP = 100;
    public int Damage = 30;

    private Transform PlayerPosition;
    public float hitTimer;
    public float attackCD;
    [SerializeField] private Transform attackHitBox;
    [SerializeField] private LayerMask playerMask;
    public float attackRadius = 100f;
    private Vector3 Scale;

    
    void Start()
    {
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ani = GetComponent<Animator>();
        Scale = transform.localScale;
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - PlayerPosition.position.x) < 400 && attackCD <= 0)
        {
            ani.Play("Attack");
            attackCD = 2.5f;
            Invoke("DoAttack", 0.5f);
        }
        if (hitTimer > 0)
            hitTimer -= Time.deltaTime;
        if (attackCD > 0)
            attackCD -= Time.deltaTime;
        if (hitTimer <= 0 && HP > 0 && attackCD < 1.5)
        {
            if (Mathf.Abs(transform.position.x - PlayerPosition.position.x) >= 200)
            {
                transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.position, speed * Time.deltaTime);
                ani.Play("Walk");
            }
                if (PlayerPosition.position.x - transform.position.x > 0)
                    transform.localScale = new Vector3(-Scale.x, Scale.y, Scale.z);
                else
                    transform.localScale = new Vector3(Scale.x, Scale.y, Scale.z);
        }
        else if (hitTimer > 0 && HP > 0 && attackCD < 1.5)
            ani.Play("Hurt");
        if (HP <= 0)
            Invoke("Death", 0.3f);
    }


    void Death()
    {
        ani.Play("Death");
        Destroy(gameObject, 1f);
    }

    void DoAttack()
    {
        Collider2D Player = Physics2D.OverlapCircle(attackHitBox.position, attackRadius, playerMask);
        if (Player != null)
        {
            Player.gameObject.GetComponent<Move>().HP -= Damage;
            Player.gameObject.GetComponent<Move>().hitTimer = 0.3f;
            if (transform.localScale.x > 0)
            {
                Player.GetComponent<Rigidbody2D>().velocity = new Vector2(-800f, 1000f);
            }
            else
            {
                Player.GetComponent<Rigidbody2D>().velocity = new Vector2(800f, 1000f);
            }
        }

    }
}
