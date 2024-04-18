using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameManager game;
    public bool OnTheFloor = false;

    // Player 

    public float fallSpeed;
    public float moveSpeed;
    public float jumpForce;

    // Attack
    public GameObject attaqueFaible;
    public GameObject attaqueForte;


    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
