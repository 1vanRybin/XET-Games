using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PauseMenuController : MonoBehaviour
{
    [FormerlySerializedAs("Canvas")] public Canvas canvas;
    public GameObject Panel;
    public void Start()
    {
        canvas.enabled = false;
    }

    void Update()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

        if (Input.GetButtonDown("OpenPauseMenu"))
        {
            Panel.SetActive(false);
            canvas.enabled = !canvas.enabled;
        }
    }
    
    public void SwapScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
