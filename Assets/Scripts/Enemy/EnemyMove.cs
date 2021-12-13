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
    Vector3 findPos;
    public float hitTimer;
    public float attackCD;
    private bool alive = true;
    [SerializeField] private Transform attackHitBox;
    [SerializeField] private LayerMask playerMask;
    public float attackRadius = 250f;
    private Vector3 Scale;

    
    void Start()
    {
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ani = GetComponent<Animator>();
        Scale = transform.localScale;
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - PlayerPosition.position.x) < 400 && attackCD <= 0 && Mathf.Abs(transform.position.y - PlayerPosition.position.y) <= 50)
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
                findPos = new Vector3(PlayerPosition.position.x, transform.position.y, PlayerPosition.position.z);
                transform.position = Vector3.MoveTowards(transform.position, findPos, speed * Time.deltaTime);
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
        if (alive == true)
        {
            GameObject.Find("RoomPool").GetComponent<RoomPool>().doorEnable--;
            alive = false;
        }
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

    private void OnBecameVisible()
    {
        GameObject.Find("RoomPool").GetComponent<RoomPool>().doorEnable++;
    }
}
