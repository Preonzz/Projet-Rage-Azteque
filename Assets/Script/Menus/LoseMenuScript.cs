using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenuScript : MonoBehaviour
{
    public void PlayOnceMoreButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }
    public void BackToTheBegginingButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
