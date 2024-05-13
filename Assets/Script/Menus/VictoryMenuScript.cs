using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMenuScript : MonoBehaviour
{
    public void BackToTheMainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }
}
