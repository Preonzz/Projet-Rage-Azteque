using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckFloor : MonoBehaviour
{
    PlayerManager player;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Floor" && GameManager.Instance.player.OnTheFloor == false)
        {
            GameManager.Instance.player.OnTheFloor = true;
            GameManager.Instance.player.animator.SetBool("EnSaut", false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Floor" && GameManager.Instance.player.OnTheFloor == true)
        { 
            GameManager.Instance.player.OnTheFloor = false;
            GameManager.Instance.player.animator.SetBool("EnSaut", true);
        }
    }
}
