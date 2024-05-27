using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SniperController : MonoBehaviour
{
    public GameObject player;
    public GameObject sniper;
    Vector3 playerPosition;
    public int HP;
    public Rigidbody2D enemyBody;
    public float enemyAttackRange;
    public float enemySpeed;
    public Vector2 SpawnPosition;
    int freezeTime = 0;
    bool isFreeze = false;
    float XEnemyPosition;

    public Animator animEnemy;
    bool dead = false;

    //graphic
    public GameObject charaMelee;
    float scaleCharaMelee;
    float scaleCharaMemoryMelee;

    //enemy dans le soleil
    bool enemyInSun = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Aim());
        StartCoroutine(SunPulse());
        scaleCharaMelee = charaMelee.transform.localScale.x;
        scaleCharaMemoryMelee = scaleCharaMelee;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HP <= 0)
        {
            dead = true;
            Destroy(sniper);
            freezeTime = 5;
            StartCoroutine(Death());
            GameManager.Instance.player.enemyKilled += 1;
            GameManager.Instance.camera.isCameraShaking = false;
        }


            if (XEnemyPosition < player.transform.position.x && player.transform.position.x - XEnemyPosition > enemyAttackRange)
            {
                scaleCharaMelee = -scaleCharaMemoryMelee;
            }
            //déplacement à gauche
            if (XEnemyPosition > player.transform.position.x && XEnemyPosition - player.transform.position.x > enemyAttackRange)
            {
                scaleCharaMelee = scaleCharaMemoryMelee;
            }
            charaMelee.transform.localScale = new Vector2(scaleCharaMelee, charaMelee.transform.localScale.y);
        XEnemyPosition = transform.position.x;
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Light Attack"))
        {
            HP -= 10;
            isFreeze = true;
            freezeTime += 2;
            StartCoroutine(GameManager.Instance.camera.ShakeCamera(UnityEngine.Random.Range(0.5f, 0.7f), 0.1f));
            GameManager.Instance.player.currentRage += 2;
            Debug.Log(HP);
            if (GameManager.Instance.player.lastAxis >= 0.1f)
            {
                enemyBody.velocity = new Vector2(2, 1);
            }
            if (GameManager.Instance.player.lastAxis <= -0.1f)
            {
                enemyBody.velocity = new Vector2(-2, 1);
            }
        }


        if (other.gameObject.tag == ("Heavy Attack"))
        {
            HP -= 25;
            isFreeze = true;
            freezeTime += 3;
            StartCoroutine(GameManager.Instance.camera.ShakeCamera(UnityEngine.Random.Range(1.2f, 1.5f), 0.2f));
            GameManager.Instance.player.currentRage += 4;

            Debug.Log(HP);
            if (GameManager.Instance.player.lastAxis >= 0.1f)
            {
                enemyBody.velocity = new Vector2(5, 1);
            }
            if (GameManager.Instance.player.lastAxis <= -0.1f)
            {
                enemyBody.velocity = new Vector2(-5, 1);
            }

        }
        if (other.gameObject.tag == ("Sun Beam"))
        {
            enemyInSun = true;
            Debug.Log("true");
        }
        if (GameManager.Instance.player.currentRage > GameManager.Instance.player.maxRage)
        {
            GameManager.Instance.player.currentRage = GameManager.Instance.player.maxRage;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Sun Beam"))
        {
            enemyInSun = false;
            Debug.Log("false");
            if (GameManager.Instance.player.currentRage > GameManager.Instance.player.maxRage)
            {
                GameManager.Instance.player.currentRage = GameManager.Instance.player.maxRage;
            }
        }
    }

    IEnumerator SunPulse()
    {
        yield return new WaitUntil(() => enemyInSun == true);
        HP -= GameManager.Instance.player.sunDmg;
        StartCoroutine(GameManager.Instance.camera.ShakeCamera(UnityEngine.Random.Range(0.5f, 0.7f), 0.1f));
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

    IEnumerator InFreeze()
    {

        yield return new WaitUntil(() => freezeTime != 0);

        yield return new WaitForSecondsRealtime(0.3f);
        freezeTime -= 1;

        if (freezeTime == 0)
        {
            isFreeze = false;
        }

        StartCoroutine(InFreeze());
    }
    IEnumerator Aim()
    {
        playerPosition = player.transform.position;
        Vector3 direction = playerPosition - sniper.transform.position;
        Quaternion aimRotation = Quaternion.LookRotation(direction);
        sniper.transform.rotation = aimRotation;
        yield return new WaitForSecondsRealtime(0.1f);

        StartCoroutine(Aim());
    }

    IEnumerator Death()
    {
        dead = true;
        animEnemy.Play("Base Layer.death");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
