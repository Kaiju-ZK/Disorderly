using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private Camera cam;
    private bool once = false;

    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Room").GetComponent<RoomPool>().count != GameObject.FindGameObjectWithTag("Room").GetComponent<RoomPool>().counter - 1)
            if (!once)
            {
                GameObject.Find("RoomPool").GetComponent<RoomPool>().Doors[GameObject.Find("RoomPool").GetComponent<RoomPool>().doorCount] = gameObject;
                GameObject.Find("RoomPool").GetComponent<RoomPool>().doorCount++;
                once = true;
            }
        if (GameObject.FindGameObjectWithTag("Room").GetComponent<RoomPool>().count == GameObject.FindGameObjectWithTag("Room").GetComponent<RoomPool>().counter - 1)
            if (!once && playerChangePos.x < 0)
            {
                GameObject.Find("RoomPool").GetComponent<RoomPool>().Doors[GameObject.Find("RoomPool").GetComponent<RoomPool>().doorCount] = gameObject;
                GameObject.Find("RoomPool").GetComponent<RoomPool>().doorCount++;
                once = true;
            }
        if (GameObject.Find("RoomPool").GetComponent<RoomPool>().doorEnable > 0)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position += playerChangePos;
            cam.transform.position += cameraChangePos;
        }
    }
}
