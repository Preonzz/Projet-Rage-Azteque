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

    // Player 

    public float fallSpeed;
    public float moveSpeed;
    public float jumpForce;

    // Attack
    public GameObject attaqueFaible;
    public GameObject attaqueForte;
    public float lastAxis = 1;

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
    }
}
