using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pad : MonoBehaviour
{
    public Canvas CanvasPad;
    public static Canvas pad;

    private void Start()
    {
        pad = CanvasPad;
        CanvasPad.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("OpenClosePad"))
            CanvasPad.enabled = !CanvasPad.enabled;
    }
}
