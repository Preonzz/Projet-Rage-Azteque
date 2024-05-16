using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool pause = false;

    public GameObject PauseMenuUI;
    void Update()
    {
        if (Input.GetButtonDown("PauseButton"))
        {
            if (pause == true)
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
        pause = false;
    }

    void PauseGame ()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
    }
}
