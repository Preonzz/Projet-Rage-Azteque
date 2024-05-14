using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public int minKill;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Ouvrir());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Ouvrir()
    {
        yield return new WaitForSeconds(0.1f);
        if (GameManager.Instance.player.enemyKilled >= minKill)
        {
            Destroy(gameObject);
        }
        StartCoroutine(Ouvrir());
    }

}
