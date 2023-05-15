using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogTrigerText : MonoBehaviour
{ 
    [SerializeField] private Text hintText;
    [SerializeField] string text;
    [SerializeField] Color textColor;
    private void Start()
    {
        hintText.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            hintText.color = textColor;
            hintText.text = text;
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
