using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerManager player; 

    public Rigidbody2D body;
    public float vitesse = 10;
    float TimerSaut;
    public float TempsSaut = 0.1f;
    bool jumping = false;

    void Start()
    {
        player = GetComponent<PlayerManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (player.OnTheFloor == true && Input.GetButtonDown("Jump"))
        {
            body.velocity = Vector2.up * player.jumpForce;
            jumping = true;
            TimerSaut = TempsSaut;
        }
        if (TimerSaut > 0)
        {
            TimerSaut -= Time.deltaTime;
        }

        if (jumping == true && Input.GetButton("Jump"))
        {
            if (TimerSaut > 0)
            {
                body.velocity = Vector2.up * player.jumpForce * 2;
            }
        }
        else
        {
            jumping = false;
        }

        if (jumping == false || TimerSaut <= 0)
        {
            body.AddForce(Vector2.up * player.fallSpeed);
        }
    }
    private void FixedUpdate()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * vitesse, body.velocity.y);


    }

}

