using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DummyController : MonoBehaviour
{
    public int HP;
    public Rigidbody2D enemyBody;
    
    
    //enemy dans le soleil
    bool enemyInSun = false;
    
    void Start()
    {
        StartCoroutine(SunPulse());
    }
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Light Attack"))
        {
            HP -= 10;
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

    
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Sun Beam"))
        {
            enemyInSun = false;
            Debug.Log("false");
        }
    }

    IEnumerator SunPulse()
    {
        yield return new WaitUntil(() => enemyInSun == true);
        HP -= 4;
        Debug.Log(HP);
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

}

