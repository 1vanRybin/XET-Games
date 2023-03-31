using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TriggerText : MonoBehaviour
{
    [SerializeField] private Text chargeText;
    [SerializeField] private Text chargeLevel;
    public static int count;

    private void Start()
    {
        chargeText.enabled = false;
    }

    private void Update()
    {
        chargeLevel.color = (count <= 10) ?
             Color.red: Color.green;
        chargeLevel.text = count.ToString();
    }

    private void Start()
    {
        chargeText.enabled = false;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            chargeText.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E) && count<100)
                count++;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            chargeText.enabled = false;
    }

}
