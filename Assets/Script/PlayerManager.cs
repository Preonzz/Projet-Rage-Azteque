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
    public int currentRage;
    public int maxRage;
    public int RageSections;
    public int combo = 3;

    // Player 

    public float fallSpeed;
    public float moveSpeed;
    public float jumpForce;
    float XPlayerPosition;
    public bool mort = false;

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

    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.Instance;
        StartCoroutine(PositionX());
    }

    IEnumerator PositionX()
    {
        XPlayerPosition = transform.position.x;
        yield return null;
        StartCoroutine(PositionX());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("EnemySmash"))
        {
            Health -= 10;
        }

        if (Health <= 0)
        {
            mort = true;
        }
    }
}
