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

    //enemy dans le soleil
    bool enemyInSun = false;
    
    void Start()
    {
        StartCoroutine(SunPulse());
        StartCoroutine(EnemyAttack());
    }
    void FixedUpdate()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        XEnemyPosition = transform.position.x;

        if (XEnemyPosition < player.transform.position.x && player.transform.position.x - XEnemyPosition > enemyAttackRange) 
        {
            Debug.Log("Droite");
            enemyBody.velocity = new Vector2(enemySpeed, enemyBody.velocity.y);
        }

        if (XEnemyPosition > player.transform.position.x && XEnemyPosition - player.transform.position.x > enemyAttackRange)
        {
            Debug.Log("Gauche");
            enemyBody.velocity = new Vector2(-enemySpeed, enemyBody.velocity.y);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Light Attack"))
        {
            HP -= 10;
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
        Debug.Log(HP);
        GameManager.Instance.player.currentRage += 1;
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
        yield return new WaitUntil(() => player.transform.position.x - XEnemyPosition < enemyAttackRange && player.transform.position.x - XEnemyPosition > -enemyAttackRange);
        Debug.Log("attack ennemie");
        if (player.transform.position.x - XEnemyPosition > 0)
        {
            SpawnPosition = new Vector2(transform.position.x + 0.8f, transform.position.y);
            Attack = Instantiate(enemyAttack, SpawnPosition, Quaternion.identity);
        }

        if (player.transform.position.x - XEnemyPosition < 0)
        {
            SpawnPosition = new Vector2(transform.position.x - 0.8f, transform.position.y);
            Attack = Instantiate(enemyAttack, SpawnPosition, Quaternion.identity);
        }
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(Attack);

        yield return new WaitForSecondsRealtime(1f);

        StartCoroutine(EnemyAttack());
    }
}

