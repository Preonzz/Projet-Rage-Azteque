using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutelScripts : MonoBehaviour
{
    PlayerManager playerManager;
    public bool Active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Active == true)
        {
            Active = false;
        }
    }
}
