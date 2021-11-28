using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseMove : MonoBehaviour
{
    private float way = 0;
    [SerializeField] private float speed = 5f;
    private float sp;
    private RectTransform size;
    private float HP;

    void Awake()
    {
        size = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        float ex;
        HP = NewMovement.HP;
        if (HP <= 60)
            ex = 4;
        else if (HP <= 120)
            ex = 4 / (HP / 60);
        else
            ex = 4 / 2;
        sp = (float)(speed * (HP / 60));
        if (sp > speed * 2)
            sp = speed * 2;
        size.sizeDelta = new Vector2(ex, 4);
        transform.Translate(-sp, 0, 0);
        way += sp * Time.deltaTime;
        if (way >= 30)
            Object.Destroy(gameObject);
    }
}
