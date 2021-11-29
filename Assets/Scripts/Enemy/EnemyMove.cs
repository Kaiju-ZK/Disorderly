using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int HP = 100;
    public float hitTimer;

    private Animator ani;
    
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        hitTimer -= Time.deltaTime;
        if (hitTimer > 0 && HP > 0)
            ani.Play("Hurt");
        if (HP <= 0)
            Invoke("Death", 0.3f);
    }


    void Death()
    {
        ani.Play("Death");
        Destroy(gameObject, 1f);
    }
}
