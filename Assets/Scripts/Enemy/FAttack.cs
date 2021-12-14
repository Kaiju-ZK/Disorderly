using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAttack : MonoBehaviour
{
    [SerializeField] private float way = 1000f;
    [SerializeField] private float speed = 700f;
    private int Damage = 20;
    private GameObject Player;
    private float move;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        move = transform.position.x;
    }

    void FixedUpdate()
    {
        float move = transform.position.x - speed * Time.deltaTime;
        transform.position = new Vector3 (move, transform.position.y, -0.1f);
        way -= Mathf.Abs(speed) * Time.deltaTime;
        if (way <= 0)
            Object.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.GetComponent<Move>().HP -= Damage;
            Player.GetComponent<Move>().hitTimer = 0.3f;
            if (transform.localScale.x > 0)
            {
                Player.GetComponent<Rigidbody2D>().velocity = new Vector2(-800f, 1000f);
            }
            else
            {
                Player.GetComponent<Rigidbody2D>().velocity = new Vector2(800f, 1000f);
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
