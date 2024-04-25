using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DummyController : MonoBehaviour
{
    public int HP;
    
    void Start()
    {
        
    }
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Light Attack"))
        {
            HP -= 10;
            Debug.Log(HP);
        }

        if (other.gameObject.tag == ("Heavy Attack"))
        {
            HP -= 25;
            Debug.Log(HP);
        }
    }
}

