using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutelScripts : MonoBehaviour
{
    PlayerManager playerManager;
    public bool Active;
    public ParticleSystem particule;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Active == true)
        {
            Active = false;
            particule.Stop();
            GameManager.Instance.autelManager.Autel(); 
        }
    }
}
