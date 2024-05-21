using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutelScripts : MonoBehaviour
{
    PlayerManager playerManager;
    public bool Active;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touché l'autel");
        if (other.gameObject.tag == "Player" && Active == true)
        {
            Debug.Log("Activé");
            Active = false;
            GameManager.Instance.autelManager.Autel(); 
        }
    }
}
