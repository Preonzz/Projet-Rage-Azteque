using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public RawImage screen;
    public Image etoile;
    public Animator animator;
    void Start()
    {
        StartCoroutine(video());
        etoile.enabled = false;
    }
    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }

    IEnumerator video()
    {
        yield return new WaitForSecondsRealtime(34);
        Destroy(screen);
        etoile.enabled = true;
        animator.Play("Base Layer.disparition");
    }
}
