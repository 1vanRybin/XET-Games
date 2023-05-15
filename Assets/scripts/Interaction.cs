using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] Text helpText;
    [SerializeField] string inputText;
    [SerializeField] Color textColor;
    [SerializeField] Image startBattle;

    private void Start()
    {
        startBattle.enabled = false;
    }
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
            helpText.enabled=false;
            startBattle.enabled = true;
           Invoke(nameof(Loader),2f);
        }
    }

    private void Loader()
    {
        SceneManager.LoadScene("Fighting Scene");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        helpText.enabled = false;
    }
}
