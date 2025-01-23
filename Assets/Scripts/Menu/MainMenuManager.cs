using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game_1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
