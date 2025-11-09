using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

<<<<<<< Updated upstream
=======
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game...");
    }

>>>>>>> Stashed changes
}
