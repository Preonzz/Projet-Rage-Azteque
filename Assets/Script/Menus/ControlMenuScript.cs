using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenuScript : MonoBehaviour
{
    public GameObject ControlMenuUI;
    void Start()
    {
        ControlMenuUI.SetActive(true);
        StartCoroutine(CloseControlMenu());
        Time.timeScale = 0f;
    }

    public IEnumerator CloseControlMenu()
    {
        yield return new WaitForSecondsRealtime(1f);
        yield return new WaitUntil(() => Input.GetButton("Jump"));

        ControlMenuUI.SetActive(false);
        Time.timeScale= 1f;

        yield return null;
    }
}
