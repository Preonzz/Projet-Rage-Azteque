using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperController : MonoBehaviour
{
    public GameObject player;
    public GameObject sniper;
    Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Aim());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Aim()
    {
        playerPosition = player.transform.position;
        Vector3 rotation = playerPosition - sniper.transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        sniper.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        Debug.Log("ça marche");
        yield return new WaitForSecondsRealtime(0.1f);

        StartCoroutine(Aim());
    }
}
