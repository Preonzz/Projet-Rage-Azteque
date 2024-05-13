using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgainMenu : MonoBehaviour
{
    public void BackToMainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgainButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }
}
