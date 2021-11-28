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
        if (trigger && Input.GetKeyDown(KeyCode.E))
        {
            Player.transform.position += playerChangePos;
            cam.transform.position += cameraChangePos;
        }
    }
}
