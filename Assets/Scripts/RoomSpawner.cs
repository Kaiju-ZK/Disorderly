using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public enum Exit
    {
        Up,
        Center,
        Down
    }
    public enum Enter
    {
        Up,
        Center,
        Down
    }
    
    public Exit exit;
    private RoomPool variants;
    [SerializeField] private Transform sideDoorPosition;
    [SerializeField] private Transform sidePosition;
    [SerializeField] private Transform sideDoor;
    [SerializeField] private Transform exitDoor;
    private int rand;
    private bool isSpawned = false;
    private bool isSide;
    //private readonly float waitTime = 3f;

    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Room").GetComponent<RoomPool>();
        Invoke("Spawn", 0.3f);
        variants.counter++;
    }

    public void Spawn()
    {
        if (variants.counter <= variants.count)
        {
            if (exit == Exit.Up)
            {
                rand = Random.Range(0, variants.UpEnter.Length);
                Instantiate(variants.UpEnter[rand], transform.position, variants.UpEnter[rand].transform.rotation);
            }
            else if (exit == Exit.Center)
            {
                rand = Random.Range(0, variants.CenterEnter.Length);
                Instantiate(variants.CenterEnter[rand], transform.position, variants.CenterEnter[rand].transform.rotation);
            }
            else if (exit == Exit.Down)
            {
                rand = Random.Range(0, variants.DownEnter.Length);
                Instantiate(variants.DownEnter[rand], transform.position, variants.DownEnter[rand].transform.rotation);
            }
        }
        if (variants.counter <= variants.count + 1)
        for (int i = 0; i < variants.side.Count; i++)
        {
            if (variants.side[i] == variants.counter)
            {
                Instantiate(sideDoor, sideDoorPosition.position, sideDoor.transform.rotation);
                if (variants.type[i] == 1)
                {
                    rand = Random.Range(0, variants.Weapon.Length);
                    Instantiate(variants.Weapon[rand], sidePosition.position, variants.Weapon[rand].transform.rotation);
                }
                else if (variants.type[i] == 2)
                {
                    rand = Random.Range(0, variants.Medicine.Length);
                    Instantiate(variants.Medicine[rand], sidePosition.position, variants.Medicine[rand].transform.rotation);
                }
                else if (variants.type[i] == 3)
                {
                    rand = Random.Range(0, variants.Single.Length);
                    Instantiate(variants.Single[rand], sidePosition.position, variants.Single[rand].transform.rotation);
                }
                else if (variants.type[i] == 4)
                {
                    rand = Random.Range(0, variants.Electricity.Length);
                    Instantiate(variants.Electricity[rand], sidePosition.position, variants.Electricity[rand].transform.rotation);
                }
                else if (variants.type[i] == 5)
                {
                    rand = Random.Range(0, variants.Boss.Length);
                    Instantiate(variants.Boss[rand], sidePosition.position, variants.Boss[rand].transform.rotation);
                }
            }
        }
        Destroy(gameObject);
        Destroy(sideDoorPosition.gameObject);
        if (variants.count == variants.counter - 1)
            Destroy(exitDoor.gameObject);
    }
}
