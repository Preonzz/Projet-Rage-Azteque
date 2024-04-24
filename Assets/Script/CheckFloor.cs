using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckFloor : MonoBehaviour
{
    PlayerManager player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Floor" && GameManager.Instance.player.OnTheFloor == false)
        {
            Debug.Log(GameManager.Instance.player.OnTheFloor);
            GameManager.Instance.player.OnTheFloor = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Floor" && GameManager.Instance.player.OnTheFloor == true)
        {
            Debug.Log(GameManager.Instance.player.OnTheFloor);
            GameManager.Instance.player.OnTheFloor = false;
        }
    }
}
