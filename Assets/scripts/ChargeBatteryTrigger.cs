using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBatteryTrigger : MonoBehaviour
{
    [SerializeField] private Text helpText;
    [SerializeField] private Text chargeLevel;
    [SerializeField] string inputText;
    [SerializeField] Color textColor;
    public static int count;

    private void Update()
    {
        chargeLevel.color = (count <= 10) ?
             Color.red: Color.green;
        chargeLevel.text = count.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        helpText.text = inputText;
        helpText.color = textColor;
        helpText.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E) && count<100)
            count++;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        helpText.enabled = false;
    }

}