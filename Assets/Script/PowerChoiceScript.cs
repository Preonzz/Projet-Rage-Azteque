using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerChoiceScript : MonoBehaviour
{
    
    public void Autel()
    {
        
    }

    public void unlockSunBeamButton()
    {
        GameManager.Instance.player.unlockSun = true;
    }
    public void unlockRageButton()
    {
        GameManager.Instance.player.unlockRage = true;
    }


    public void unlockHealButton()
    {
        GameManager.Instance.player.unlockHeal = true;
    }
}
