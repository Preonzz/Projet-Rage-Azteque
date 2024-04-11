using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerManager player; 

    float inputHorizontal;
    Vector2 playerVelocity;
    public Rigidbody2D body;

    void Start()
    {
        player = GetComponent<PlayerManager>();
        StartCoroutine(Jump());
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal") ;
        playerVelocity = new Vector2(inputHorizontal * player.moveSpeed, body.velocity.y * player.fallSpeed);

        body.velocity = playerVelocity;

    }

    private IEnumerator Jump()
    {
        yield return new WaitUntil(() => player.OnTheFloor = true);
        yield return new WaitUntil(() => Input.GetButtonDown("Jump"));

        Debug.Log("Jump");
        playerVelocity = new Vector2(inputHorizontal * player.moveSpeed, body.velocity.y);

        StartCoroutine(Jump());
    }
}
