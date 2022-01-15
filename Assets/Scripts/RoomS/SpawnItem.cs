using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject[] Items;
    private bool spawned = false;

    public void Appear()
    {
        if (!spawned)
        {
            int rand;
            rand = Random.Range(0, Items.Length);
            Instantiate(Items[rand], transform.position, Items[rand].transform.rotation);
            spawned = true;
        }
    }
}
