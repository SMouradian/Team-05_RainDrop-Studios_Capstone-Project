using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject container; 

    private bool isPaused = false;

    void Start()
    {
        // Make sure game starts unpaused and menu hidden
        if (container != null)
            container.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        if (container != null)
            container.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;

        // optional: unlock & show cursor if you’re using mouse look
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        if (container != null)
            container.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

       
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }


}




