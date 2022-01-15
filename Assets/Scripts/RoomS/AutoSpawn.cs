using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawn : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<SpawnItem>().Appear();
    }
}
