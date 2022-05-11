using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{

    public Collision2D infoCollision;

    private void OnCollisionEnter2D(Collision2D collision2D) {
        Debug.Log("Projectile experienced collision!");
        infoCollision = collision2D;
    }
}
