using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] Text interaction;
    [SerializeField] Text helpText;
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
        if (Input.GetKey(KeyCode.E))
        {
            interaction.enabled = true;
            helpText.enabled=false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        helpText.enabled = false;
        interaction.enabled = false;
    }
}
