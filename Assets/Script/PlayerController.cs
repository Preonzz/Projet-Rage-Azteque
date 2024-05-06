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
    float interAttack = 1f;

    public Vector2 SpawnPosition;

    GameObject smallAttack;
    GameObject bigAttack;
    bool inAttack = false;
    bool inAttack2 = false;
    bool stopAttack = false;

    GameObject SunRay;

    void Start()
    {
        rememberSpeedX = speedX;
        player = GetComponent<PlayerManager>();
        StartCoroutine(lightAttack());
        StartCoroutine(rememberAxis());
        StartCoroutine(HeavyAttack());
        StartCoroutine(Sun());
        StartCoroutine(Rage());
        StartCoroutine(Heal());

    }
    // Update is called once per frame
    void Update()
    {
        if (player.OnTheFloor == true && Input.GetButtonDown("Jump") && stopAttack == false && player.mort == false)
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
        if (player.mort == false)
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, body.velocity.y);
            move();
        }
        

        if (inAttack == false)
        {
            speedOnAir();
        }

    }

    // Deplacements 
    IEnumerator rememberAxis()
    {
        yield return new WaitUntil(() => Input.GetAxis("Horizontal") < -0.1 || Input.GetAxis("Horizontal") > 0.1);
        if (Input.GetAxis("Horizontal") < -0.1 || Input.GetAxis("Horizontal") > 0.1)
        {
            player.lastAxis = Input.GetAxis("Horizontal");
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
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, 0.5f);
        }
    }
    // Attaque 

    IEnumerator lightAttack()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1") && inAttack == false && player.mort == false);
        if (player.lastAxis >= 0.1f)
        {
            SpawnPosition = new Vector2(transform.position.x+0.8f, transform.position.y);
            smallAttack = Instantiate(player.attaqueFaible, SpawnPosition, Quaternion.identity);
        }

        if (player.lastAxis <= -0.1f)
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




        yield return new WaitForSecondsRealtime(0.2f / player.attackSpeed);

        
        inAttack = false;
        yield return null;
        StartCoroutine(lightAttack());
    }

    IEnumerator HeavyAttack()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Fire2") && inAttack2 == false && player.mort == false);
        if (player.lastAxis >= 0.1f)
        {
            SpawnPosition = new Vector2(transform.position.x + 0.8f, transform.position.y);
            bigAttack = Instantiate(player.attaqueForte, SpawnPosition, Quaternion.identity);
        }

        if (player.lastAxis <= -0.1f)
        {
            SpawnPosition = new Vector2(transform.position.x - 0.8f, transform.position.y);
            bigAttack = Instantiate(player.attaqueForte, SpawnPosition, Quaternion.identity);
        }
        inAttack2 = true;
        stopAttack = true;
        speedX = 1;
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(bigAttack);
        speedOnAir();
        stopAttack = false;
        yield return new WaitForSecondsRealtime(1f / player.attackSpeed);

        inAttack2 = false;
        yield return null;
        StartCoroutine(HeavyAttack());
    }

    // Powers 
    IEnumerator Sun()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Fire3") && inAttack == false && inAttack2 == false && player.unlockSun == true && player.currentRage >= 60 && player.mort == false);

        player.currentRage -= 60;
        if (player.lastAxis >= 0.1f)
        {
            SpawnPosition = new Vector2(transform.position.x + 10f, transform.position.y);
            SunRay = Instantiate(player.sunBeam, SpawnPosition, Quaternion.identity);
        }

        if (player.lastAxis <= -0.1f)
        {
            SpawnPosition = new Vector2(transform.position.x - 10f, transform.position.y);
            SunRay = Instantiate(player.sunBeam, SpawnPosition, Quaternion.identity);
        }
        inAttack = true;
        inAttack2 = true;
        stopAttack = true;
        speedX = 0;
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(SunRay);
        speedOnAir();
        stopAttack = false;
        yield return new WaitForSecondsRealtime(1.5f / player.attackSpeed);

        inAttack = false;
        inAttack2 = false;
        yield return null;

        StartCoroutine(Sun());
    }

    IEnumerator Rage()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Fire4") && player.enraged == false && player.unlockRage == true && player.currentRage >= 40 && player.mort == false);
        player.currentRage -= 40;
        player.enraged = true;
        player.attackSpeed = 5;
        Debug.Log("Rage");
        yield return new WaitForSecondsRealtime(player.rageDuration);
        player.enraged = false;
        player.attackSpeed = 1;
        yield return null;

        StartCoroutine(Rage());
    }

    IEnumerator Heal()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Fire5") && inAttack == false && player.unlockHeal == true && player.currentRage >= 20 && player.mort == false);
        player.currentRage -= 20;
        player.Health += player.healQuantity;
        if (player.Health > player.MaxHealth)
        {
            player.Health = player.MaxHealth;
        }
        Debug.Log("Heal");
        yield return null;

        StartCoroutine(Heal());
    }
}