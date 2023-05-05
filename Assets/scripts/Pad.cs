using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pad : MonoBehaviour
{
    public Canvas CanvasPad;

    private void Start()
    {
        CanvasPad.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("OpenClosePad"))
            CanvasPad.enabled = !CanvasPad.enabled;
    }
}
