using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairMalfunctions : MonoBehaviour
{
    [SerializeField] SpriteRenderer repairedObj;
    [SerializeField] InputField input;
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
        if (Input.GetKey(KeyCode.E))
        {
            if (ChargeBatteryTrigger.ChargeLVL >= 5)
            {
                input.gameObject.SetActive(true);
                input.text = "//Сделай так, чтоб вернулось значение выражения 5+5." +
                    "\n return ...";
                helpText.enabled = false;
            }
            else
                helpText.text = "Не хватает заряда (требуется 5)";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        helpText.enabled = false;
    }

    public void CheckResult()
    {
        var code = CodeCompiler.Execute(input.text).ToString();
        if (code == "10")
        {
            repairedObj.sortingOrder = 2;
            helpText.fontSize = 30;
            Destroy(gameObject);
            Destroy(input.gameObject);
            ChargeBatteryTrigger.ChargeLVL -= 5;
        }

        else
        {
            helpText.fontSize = 12;
            helpText.enabled = true;
            helpText.text = "Wrong answer: " + code + "\nExpected other.";
        }
    }
}


