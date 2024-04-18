using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerManager player; 

    float inputHorizontal;
    Vector2 playerVelocity;
    public Rigidbody2D body;
    public float vitesse = 10;
    bool SurSol = false;
    public Transform PositionPieds;
    public float rayon;
    public LayerMask sol;
    bool EnSaut = false;
    float TimerSaut;
    public float TempsSaut = 1.5f;

    void Start()
    {
        player = GetComponent<PlayerManager>();
    }




    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        SurSol = Physics2D.OverlapCircle(PositionPieds.position, rayon, sol);
        if (player.OnTheFloor == true && Input.GetButtonDown("Jump"))
        {
            body.velocity = Vector2.up * player.jumpForce * 2;
            player.OnTheFloor = true;
            TimerSaut = TempsSaut;
        }

        if (player.OnTheFloor == true && Input.GetButton("Jump"))
        {
            if (TimerSaut > 0)
            {
                body.velocity = Vector2.up * player.jumpForce * 2;
                TimerSaut -= Time.deltaTime;
            }

            else
            {
                player.OnTheFloor = false;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            player.OnTheFloor = false;
        }
    }
    private void FixedUpdate()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * vitesse, body.velocity.y);


    }

}

