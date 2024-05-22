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
    int TimerSaut;
    public int TempsSaut = 20;
    bool jumping = false;


    //pause
    Vector2 fallPause;
    bool pauseMemory = false;

    //graphic
    public GameObject chara;
    float scaleChara;
    float scaleCharaMemory;
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
        scaleChara = chara.transform.localScale.x;
        scaleCharaMemory = scaleChara;
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
        if (player.mort == false)
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, body.velocity.y);
            if (stopAttack == false)
            {
                body.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, body.velocity.y);
            }

            if (stopAttack == true)
            {
                body.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, 0.5f);
            }
        }

        if (Input.GetAxis("Horizontal") == 0)
        {
            GameManager.Instance.player.animator.SetBool("EnMarche", false);
        }
        else
        {
            GameManager.Instance.player.animator.SetBool("EnMarche", true);
        }
    }

    private void FixedUpdate()
    {


        if (inAttack == false && inAttack2 == false)
        {
            speedOnAir();
        }

        if (jumping == true && Input.GetButton("Jump") && stopAttack == false)
        {
            if (TimerSaut > 0)
            {
                body.velocity = new Vector2(body.velocity.x,player.jumpForce);
                TimerSaut -= 1;
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

        if (player.pause == true && pauseMemory == false)
        {
            fallPause = body.velocity;
            pauseMemory = true;
        }

        if (player.pause == false && pauseMemory == true)
        {
            body.velocity = fallPause;
            pauseMemory = false;
        }
    }

    // Deplacements 
    IEnumerator rememberAxis()
    {
        
        yield return new WaitUntil(() => Input.GetAxis("Horizontal") < -0.1 || Input.GetAxis("Horizontal") > 0.1 && player.pause == false);
        if (Input.GetAxis("Horizontal") < -0.1 || Input.GetAxis("Horizontal") > 0.1)
        {
            player.lastAxis = Input.GetAxis("Horizontal");
            if (Input.GetAxis("Horizontal") < -0.1)
            {
                scaleChara = -scaleCharaMemory;
            }

            if (Input.GetAxis("Horizontal") > 0.1)
            {
                scaleChara = scaleCharaMemory;
            }


            chara.transform.localScale = new Vector2(scaleChara, chara.transform.localScale.y);
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


    // Attaque 

    IEnumerator lightAttack()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1") && inAttack == false && player.mort == false && player.pause == false);
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
        GameManager.Instance.player.animator.Play("Base Layer.LightAttack");
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
        yield return new WaitUntil(() => Input.GetButtonDown("Fire2") && inAttack2 == false && player.mort == false && player.pause == false);
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
        GameManager.Instance.player.animator.Play("Base Layer.HeavyAttack");
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(bigAttack);
        speedOnAir();
        stopAttack = false;
        inAttack2 = false;
        yield return new WaitForSecondsRealtime(1f / player.attackSpeed);

        
        yield return null;
        StartCoroutine(HeavyAttack());
    }

    // Powers 
    IEnumerator Sun()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Fire3") && inAttack == false && inAttack2 == false && player.unlockSun == true && player.currentRage >= 60 && player.mort == false && player.pause == false);

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
        yield return new WaitUntil(() => Input.GetButtonDown("Fire4") && player.enraged == false && player.unlockRage == true && player.currentRage >= 40 && player.mort == false && player.pause == false);
        player.currentRage -= 40;
        player.enraged = true;
        player.attackSpeed = 5;
        player.enragedParticle.Play();
        yield return new WaitForSecondsRealtime(player.rageDuration);
        player.enragedParticle.Stop();
        player.enraged = false;
        player.attackSpeed = 1;
        yield return null;

        StartCoroutine(Rage());
    }

    IEnumerator Heal()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Fire5") && inAttack == false && inAttack2 == false && player.unlockHeal == true && player.currentRage >= 40 && player.mort == false && player.Health != player.MaxHealth && player.pause == false );
        player.currentRage -= 40;
        player.Health += player.healQuantity;
        if (player.Health > player.MaxHealth)
        {
            player.Health = player.MaxHealth;
        }
        player.particule.Play();
        speedX = 0;
        yield return new WaitForSeconds(0.5f);
        speedOnAir();
        player.particule.Stop();

        StartCoroutine(Heal());
    }
}