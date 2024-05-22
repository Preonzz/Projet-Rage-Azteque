using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutelScripts : MonoBehaviour
{
    PlayerManager playerManager;
    public bool Active;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Active == true)
        {
            Active = false;
            GameManager.Instance.autelManager.Autel(); 
        }
    }
}
