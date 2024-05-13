using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.player.Health -= 10;
        }
    }
}
