using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairMalfunctions : MonoBehaviour
{
    [SerializeField] SpriteRenderer repairedObj;
    //[SerializeField] InputField input;
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
        //if (input.text.ToLower().Replace(" ", "") == "строка")
        //{
        //    helpText.enabled = false;
        //    repairedObj.sortingLayerID = 2;
        //    Destroy(this);
        //}


        if (Input.GetKey(KeyCode.E))
        {
            if (ChargeBatteryTrigger.count >= 50)
            {
                helpText.enabled = false;
                repairedObj.sortingOrder = 2;
                Destroy(this);
                ChargeBatteryTrigger.count -= 50;
            }
            helpText.text = "Не хватает заряда (требуется 50)";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        helpText.enabled = false;
    }
}


