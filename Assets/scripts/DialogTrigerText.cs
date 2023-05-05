using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogTrigerText : MonoBehaviour
{ 
    [SerializeField] private Text hintText;
    private void Start()
    {
        hintText.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            hintText.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
                hintText.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            hintText.enabled = false;
    }
}
