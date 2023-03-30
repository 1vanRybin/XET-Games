using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerText : MonoBehaviour
{
    public Text triggerText;
    public Text counter;
    int count;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            triggerText.text = "Van Sama here!!!";
            count++;
            counter.text = count.ToString();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            triggerText.text = "";
    }
}
