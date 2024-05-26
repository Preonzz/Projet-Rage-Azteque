using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DummyController : MonoBehaviour
{
    public int HP;
    public Rigidbody2D enemyBody;
    public GameObject player;
    float XEnemyPosition;
    public float enemyAttackRange;
    public float enemySpeed;
    public Vector2 SpawnPosition;
    GameObject Attack;
    public GameObject enemyAttack;
    int freezeTime = 0;
    bool isFreeze = false;
    public float meleeVision = 10;
    public Animator animEnemy;
    bool dead = false;

    //enemy dans le soleil
    bool enemyInSun = false;

    //graphic
    public GameObject charaMelee;
    float scaleCharaMelee;
    float scaleCharaMemoryMelee;

    void Start()
    {
        StartCoroutine(SunPulse());
        StartCoroutine(EnemyAttack());
        StartCoroutine (InFreeze());
        scaleCharaMelee = charaMelee.transform.localScale.x;
        scaleCharaMemoryMelee = scaleCharaMelee;
    }
    void FixedUpdate()
    {
        if (HP <= 0)
        {
            freezeTime = 5;
            StartCoroutine(Death());
            GameManager.Instance.player.enemyKilled += 1;
            GameManager.Instance.camera.isCameraShaking = false;
        }
        XEnemyPosition = transform.position.x;

        //déplacement à droite
        if (isFreeze == false && player.transform.position.x - XEnemyPosition > -meleeVision && player.transform.position.x - XEnemyPosition < meleeVision)
        {
            if (XEnemyPosition < player.transform.position.x && player.transform.position.x - XEnemyPosition > enemyAttackRange)
            {
                enemyBody.velocity = new Vector2(enemySpeed, enemyBody.velocity.y);
                scaleCharaMelee = -scaleCharaMemoryMelee;
            }
            //déplacement à gauche
            if (XEnemyPosition > player.transform.position.x && XEnemyPosition - player.transform.position.x > enemyAttackRange)
            {
                enemyBody.velocity = new Vector2(-enemySpeed, enemyBody.velocity.y);
                scaleCharaMelee = scaleCharaMemoryMelee;
            }
            charaMelee.transform.localScale = new Vector2(scaleCharaMelee, charaMelee.transform.localScale.y);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Light Attack") && dead == false)
        {
            HP -= 10;
            isFreeze = true;
            freezeTime += 2;
            StartCoroutine(GameManager.Instance.camera.ShakeCamera(UnityEngine.Random.Range(0.5f, 0.7f), 0.1f));
            GameManager.Instance.player.currentRage += 2;
            if (GameManager.Instance.player.lastAxis >= 0.1f)
            {
                enemyBody.velocity = new Vector2(2, 1);
            }
            if (GameManager.Instance.player.lastAxis <= -0.1f)
            {
                enemyBody.velocity = new Vector2(-2, 1);
            }
        }
            

        if (other.gameObject.tag == ("Heavy Attack") && dead == false)
        {
            HP -= 25;
            isFreeze = true;
            freezeTime += 3;
            StartCoroutine(GameManager.Instance.camera.ShakeCamera(UnityEngine.Random.Range(1.2f, 1.5f), 0.2f));
            GameManager.Instance.player.currentRage += 4;

            if (GameManager.Instance.player.lastAxis >= 0.1f)
            {
                enemyBody.velocity = new Vector2(5, 1);
            }
            if (GameManager.Instance.player.lastAxis <= -0.1f)
            {
                enemyBody.velocity = new Vector2(-5, 1);
            }

        }
        if (other.gameObject.tag == ("Sun Beam") && dead == false)
        {
            enemyInSun = true;
        }
        if (GameManager.Instance.player.currentRage > GameManager.Instance.player.maxRage)
        {
            GameManager.Instance.player.currentRage = GameManager.Instance.player.maxRage;
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Sun Beam") && dead == false)
        {
            enemyInSun = false;
            if (GameManager.Instance.player.currentRage > GameManager.Instance.player.maxRage)
            {
                GameManager.Instance.player.currentRage = GameManager.Instance.player.maxRage;
            }
        }
    }

    IEnumerator SunPulse()
    {
        yield return new WaitUntil(() => enemyInSun == true);
        StartCoroutine(GameManager.Instance.camera.ShakeCamera(UnityEngine.Random.Range(0.5f, 0.7f), 0.1f));
        HP -= GameManager.Instance.player.sunDmg;
        isFreeze = true;
        freezeTime += 1;
        if (GameManager.Instance.player.lastAxis >= 0.1f)
        {
            enemyBody.velocity = new Vector2(0.5f, 1);
        }
        if (GameManager.Instance.player.lastAxis <= -0.1f)
        {
            enemyBody.velocity = new Vector2(-0.5f, 1);
        }

        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(SunPulse());
    }
    IEnumerator EnemyAttack()
    {
        yield return new WaitUntil(() => player.transform.position.x - XEnemyPosition < enemyAttackRange && player.transform.position.x - XEnemyPosition > -enemyAttackRange && isFreeze == false && dead == false);
        yield return new WaitForSeconds(0.5f);

        if (isFreeze == false)
        {
            if (player.transform.position.x - XEnemyPosition > 0 )
            {
                SpawnPosition = new Vector2(transform.position.x + 0.8f, transform.position.y);
                Attack = Instantiate(enemyAttack, SpawnPosition, Quaternion.identity);
            }

            if (player.transform.position.x - XEnemyPosition < 0 )
            {
                SpawnPosition = new Vector2(transform.position.x - 0.8f, transform.position.y);
                Attack = Instantiate(enemyAttack, SpawnPosition, Quaternion.identity);
            }
            animEnemy.Play("Base Layer.EnemyAttack");
        }
        
        yield return new WaitForSecondsRealtime(0.02f);
        Destroy(Attack);

        yield return new WaitForSecondsRealtime(1.5f);

        StartCoroutine(EnemyAttack());
    }

    IEnumerator InFreeze()
    {

        yield return new WaitUntil(() => freezeTime != 0);

        yield return new WaitForSecondsRealtime(0.2f);
        freezeTime -= 1;

        if (freezeTime == 0)
        {
            isFreeze = false;
        }

        StartCoroutine(InFreeze());
    }

    IEnumerator Death()
    {
        dead = true;
        animEnemy.Play("Base Layer.DeathEnemy");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }


}

