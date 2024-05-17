using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    

    public GameObject PauseMenuUI;
    void Update()
    {
        if (Input.GetButtonDown("PauseButton"))
        {
            if (GameManager.Instance.player.pause == true)
            {
                ResumeGame();
            }

            else
            {
                PauseGame();
            }
        }
    }

    void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.player.pause = false;
    }

    void PauseGame ()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.player.pause = true;
    }

    public void BackToTheMainMenuButton()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.player.pause = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ResumeButton()
    {
        ResumeGame();
    }
}
