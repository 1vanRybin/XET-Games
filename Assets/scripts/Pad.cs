using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pad : MonoBehaviour
{
    public Canvas CanvasPad;
    
    void Update()
    {
        if (Input.GetButtonDown("OpenClosePad"))
            CanvasPad.enabled = !CanvasPad.enabled;
    }
}
