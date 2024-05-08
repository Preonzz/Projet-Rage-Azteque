using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
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
        Vector3 direction = playerPosition - sniper.transform.position;
        Quaternion aimRotation = Quaternion.LookRotation(direction);
        sniper.transform.rotation = aimRotation;
        Debug.Log("ça marche");
        yield return new WaitForSecondsRealtime(0.1f);

        StartCoroutine(Aim());
    }
}
