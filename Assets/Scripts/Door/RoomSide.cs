using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSide : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private GameObject Player;
    private bool trigger = false;
    private Camera cam;
    private bool once = false;

    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trigger = true;
            Player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trigger = false;
            Player = collision.gameObject;
        }
    }

    private void Update()
    {
        if (!once)
        {
            GameObject.Find("RoomPool").GetComponent<RoomPool>().Doors[GameObject.Find("RoomPool").GetComponent<RoomPool>().doorCount] = gameObject;
            GameObject.Find("RoomPool").GetComponent<RoomPool>().doorCount++;
            once = true;
        }
        if (GameObject.Find("RoomPool").GetComponent<RoomPool>().doorEnable > 0)
            gameObject.SetActive(false);
        if (trigger && Input.GetKeyDown(KeyCode.E))
        {
            Player.transform.position += playerChangePos;
            cam.transform.position += cameraChangePos;
        }
    }
}
