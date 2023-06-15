using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string newLevel;
    [SerializeField] private Text TextForChangeLvl;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 positionInNextScene;

    private void Start()
    {
        TextForChangeLvl.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            TextForChangeLvl.text = "E для выхода";
            TextForChangeLvl.color = Color.red;
            TextForChangeLvl.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene(newLevel);
                player.position = positionInNextScene;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
            TextForChangeLvl.enabled = false;
    }
}
