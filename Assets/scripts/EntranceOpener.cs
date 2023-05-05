using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EntranceOpener : MonoBehaviour
{
    [SerializeField] private Collider2D entrance;
    [SerializeField] private Text helpText;
    [SerializeField] string inputText;
    [SerializeField] Color textColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        helpText.text = inputText;
        helpText.color = textColor;
        helpText.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                helpText.enabled = false;
                Destroy(entrance);
                Destroy(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        helpText.enabled = false;
    }
}
