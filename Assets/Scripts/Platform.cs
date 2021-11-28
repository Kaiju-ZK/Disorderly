using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D PlatEff;

    void Start()
    {
        PlatEff = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            PlatEff.rotationalOffset = 180f;
        if (Input.GetKeyDown(KeyCode.Space))
            PlatEff.rotationalOffset = 0;
    }
}
