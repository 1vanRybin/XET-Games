using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairMalfunctions : MonoBehaviour
{
    [SerializeField] SpriteRenderer repairedObj;
    [SerializeField] InputField input;
    [SerializeField] GameObject fightPad;
    [SerializeField] private Text helpText;
    [SerializeField] string textHelp;
    [SerializeField] Color textColor;
    [SerializeField] Text task;
    [SerializeField] string taskText;
    [SerializeField] string inputText;
    [SerializeField] string answer;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        helpText.text = textHelp;
        helpText.color = textColor;
        helpText.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (ChargeBatteryTrigger.ChargeLVL >= 5)
            {
                Pad.pad.enabled = true;
                task.text = taskText;
                input.text = inputText;
                fightPad.SetActive(true);
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
        if (code == answer)
        {
            repairedObj.sortingOrder = 2;
            helpText.fontSize = 30;
            Destroy(gameObject);
            fightPad.SetActive(false);
            Pad.pad.enabled = false;
            ChargeBatteryTrigger.ChargeLVL -= 5;
        }

        else
        {
            helpText.fontSize = 25;
            helpText.enabled = true;
            helpText.text = "Wrong answer: " + code + "\nExpected other.";
        }
    }

    public void ResetCode()
    {
        input.text = inputText;
    }
}


