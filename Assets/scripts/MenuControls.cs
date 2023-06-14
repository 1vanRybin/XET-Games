using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void Update()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    public void PlayPressed(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}
