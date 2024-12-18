using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameManager game;
    public bool OnTheFloor = false;

    // Player Resources

    public int Health;
    public int MaxHealth;
    int preLife;
    public GameObject lifeBar;
    public float barOfLifeBar;
    public GameObject rageBar;
    public float barOfRageBar;
    public int currentRage;
    public int maxRage;
    public int RageSections;
    public int combo = 3;
    public ParticleSystem particule;
    public ParticleSystem enragedParticle;

    public bool pause = false;

    // Player 

    public float fallSpeed;
    public float moveSpeed;
    public float jumpForce;
    float XPlayerPosition;
    public bool mort = false;

    //animation
    public Animator animator;
    public bool EnSaut = false;

    // Attack
    public GameObject attaqueFaible;
    public GameObject attaqueForte;
    public float lastAxis = 1;
    public float attackSpeed = 1;

    // Powers
    public bool unlockSun = false;
    public bool unlockRage = false;
    public bool unlockHeal = false;

    public int sunDmg;
    public bool enraged = false;
    public int healQuantity;
    public float rageDuration;

    public GameObject sunBeam;

    //statistic
    public int enemyKilled = 0;
    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.Instance;
        barOfLifeBar = lifeBar.transform.localScale.x;
        barOfRageBar = rageBar.transform.localScale.x;
        preLife = Health;
        particule.Stop();
        enragedParticle.Stop();
        StartCoroutine(PositionX());
        StartCoroutine(LifeCursor());

    }

    IEnumerator PositionX()
    {
        XPlayerPosition = transform.position.x;
        rageBar.transform.localScale = new Vector2(barOfRageBar * currentRage / MaxHealth,lifeBar.transform.localScale.y);
        lifeBar.transform.localScale = new Vector2(barOfLifeBar * Health / maxRage, lifeBar.transform.localScale.y);
        yield return null;
        StartCoroutine(PositionX());
    }

    IEnumerator LifeCursor()
    {
        yield return new WaitUntil(() => preLife != Health );

        yield return null;
        StartCoroutine(LifeCursor());
    }

    IEnumerator Mort()
    {
        GameManager.Instance.player.animator.Play("Base Layer.CharaDeath");
        yield return new WaitForSecondsRealtime(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoseMenu");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("EnemySmash"))
        {
            Health -= 10;
            currentRage += 2;
            if (currentRage > maxRage)
            {
                currentRage = maxRage;
            }

        }

        if (collision == particule)
        {
            Health -= 5;
            currentRage += 2;
            if (currentRage > maxRage)
            {
                currentRage = maxRage;
            }

        }

        if (collision.gameObject.tag == ("Victory"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("VictoryMenu");
        }

        if (Health <= 0)
        {
            mort = true;
            StartCoroutine(Mort());
        }


    }


}
