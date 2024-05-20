using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerChoiceScript : MonoBehaviour
{
    PlayerManager player; 

    public GameObject PowerChoiceMenu;

    public GameObject SunBeamButton; 
    public GameObject RageButton;
    public GameObject HealButton;
    public void Autel()
    {
        Time.timeScale = 0f;
        PowerChoiceMenu.SetActive(true);

        SunBeamButton.SetActive(false);
        RageButton.SetActive(false);
        HealButton.SetActive(false);

        if (GameManager.Instance.player.unlockSun == false)
        {
            Debug.Log("Sun not unlocked");
            SunBeamButton.SetActive(true);
        }
        if (GameManager.Instance.player.unlockHeal == false)
        {
            HealButton.SetActive(true);
        }
        if (GameManager.Instance.player.unlockRage == false)
        {
            RageButton.SetActive(true);
        }
    }

    public void unlockSunBeamButton()
    {
        GameManager.Instance.player.unlockSun = true;
        CloseAutelMenu();

    }
    public void unlockRageButton()
    {
        GameManager.Instance.player.unlockRage = true;
        CloseAutelMenu();
    }


    public void unlockHealButton()
    {
        GameManager.Instance.player.unlockHeal = true;
        CloseAutelMenu();
    }

    public void CloseAutelMenu()
    {

        SunBeamButton.SetActive(false);
        HealButton.SetActive(false);
        RageButton.SetActive(false);

        PowerChoiceMenu.SetActive(false);

        Time.timeScale = 1f;
    }
}
