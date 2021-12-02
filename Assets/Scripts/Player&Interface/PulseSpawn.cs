using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseSpawn : MonoBehaviour
{
    [SerializeField] private GameObject pulse;
    private GameObject p;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float period;
    private float HP;
    private float time;

    private void Awake()
    {
        spawnPoint = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        p = pulse;
        time += Time.deltaTime;
        HP = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().HP;
        period = 1 / (HP / 60);
        if (time >= period)
        {
            Instantiate(pulse, spawnPoint);
            time = 0;
        }
    }
}
