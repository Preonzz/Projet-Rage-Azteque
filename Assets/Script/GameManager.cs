using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;
    }
    public PlayerManager player;
    public CameraManager camera;
    public PowerChoiceScript autelManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerManager>();
        camera = FindAnyObjectByType<CameraManager>();
        autelManager = FindAnyObjectByType<PowerChoiceScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
