using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool DeadOrAlive = false;
    void Update()
    {
        if (DeadOrAlive)
        {
            GameObject.Find("BossPool").GetComponent<SpawnItem>().Appear();
            DeadOrAlive = false;
        }
    }
}
