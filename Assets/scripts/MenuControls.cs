using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void PlayPressed(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}
