using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerManager player; 

    public Rigidbody2D body;
    public float speedX;
    float rememberSpeedX;
    public float speedY;
    float TimerSaut;
    public float TempsSaut = 0.1f;
    bool jumping = false;
    public float lastAxis = 1;
    public Vector2 SpawnPosition;
    GameObject smallAttack;
    GameObject bigAttack;
    GameObject sun;
    GameObject rage;
    GameObject heal;
    bool inAttack = false;
    bool stopAttack = false;



    void Start()
    {
        rememberSpeedX = speedX;
        player = GetComponent<PlayerManager>();
        StartCoroutine(lightAttack());
        StartCoroutine(rememberAxis());
        StartCoroutine(HeavyAttack());

    }
    // Update is called once per frame
    void Update()
    {
        if (player.OnTheFloor == true && Input.GetButtonDown("Jump") && stopAttack == false)
        {
            body.velocity = Vector2.up * player.jumpForce;
            jumping = true;
            TimerSaut = TempsSaut;
        }
        if (TimerSaut > 0)
        {
            TimerSaut -= Time.deltaTime;
        }

        if (jumping == true && Input.GetButton("Jump") && stopAttack == false)
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
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, body.velocity.y);

        if (inAttack == false)
        {
            speedOnAir();
        }
        move();
    }

    IEnumerator lightAttack()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1") && inAttack == false);
        if (lastAxis >= 0.1f)
        {
            SpawnPosition = new Vector2(transform.position.x+0.8f, transform.position.y);
            smallAttack = Instantiate(player.attaqueFaible, SpawnPosition, Quaternion.identity);
        }

        if (lastAxis <= -0.1f)
        {
            SpawnPosition = new Vector2(transform.position.x-0.8f, transform.position.y);
            smallAttack = Instantiate(player.attaqueFaible, SpawnPosition, Quaternion.identity);
        }
        inAttack = true;
        stopAttack = true;
        speedX = 1;
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(smallAttack);
        speedOnAir();
        stopAttack = false;

        yield return new WaitForSecondsRealtime(0.5f);
        
        inAttack = false;
        yield return null;
        StartCoroutine(lightAttack());
    }

    IEnumerator HeavyAttack()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Fire2") && inAttack == false);
        if (lastAxis >= 0.1f)
        {
            SpawnPosition = new Vector2(transform.position.x + 0.8f, transform.position.y);
            bigAttack = Instantiate(player.attaqueForte, SpawnPosition, Quaternion.identity);
        }

        if (lastAxis <= -0.1f)
        {
            SpawnPosition = new Vector2(transform.position.x - 0.8f, transform.position.y);
            bigAttack = Instantiate(player.attaqueForte, SpawnPosition, Quaternion.identity);
        }
        inAttack = true;
        stopAttack = true;
        speedX = 1;
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(bigAttack);
        speedOnAir();
        stopAttack = false;
        yield return new WaitForSecondsRealtime(1.5f);

        inAttack = false;
        yield return null;
        StartCoroutine(HeavyAttack());
    }

    IEnumerator rememberAxis()
    {
        yield return new WaitUntil(() => Input.GetAxis("Horizontal") < -0.1 || Input.GetAxis("Horizontal") > 0.1);
        if(Input.GetAxis("Horizontal") < -0.1 || Input.GetAxis("Horizontal") > 0.1)
        {
            lastAxis = Input.GetAxis("Horizontal");
        }
        StartCoroutine(rememberAxis());
    }


    void speedOnAir()
    {
        if (player.OnTheFloor == true)
        {
            speedX = rememberSpeedX;
        }

        if (player.OnTheFloor == false)
        {
            speedX = rememberSpeedX / 1.8f;
        }
    }

    void move()
    {
        if (stopAttack == false)
        {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, body.velocity.y);
        }

        if (stopAttack == true)
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, 0);
        }
    } 
}

public enum Powers { sun, rage, heal }

