using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSide : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private Camera cam;

    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            collision.transform.position += playerChangePos;
            cam.transform.position += cameraChangePos;
        }
    }
}
