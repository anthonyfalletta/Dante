using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpProjectile : MonoBehaviour
{
    PlayerStateManager playerStateManager;
    PlayerShooting playerShooting;
    PlayerProjectileBehavior playerProjectileBehavior;

    // Start is called before the first frame update
    void Start()
    {
        playerStateManager = GameObject.Find("Player").GetComponent<PlayerStateManager>();
        playerShooting = GameObject.Find("Player").GetComponent<PlayerShooting>();
        playerProjectileBehavior = GameObject.Find("Player").GetComponent<PlayerProjectileBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If Projectile.free or movingBack + Player Moving and hits Trigger Collider -> Destroy Projectile
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("PickUp Trigger Activated");
        if (playerStateManager.CheckPlayerState("Shooting") == false || playerShooting.GetProjectileState("MovingBack") || playerShooting.GetProjectileState("Free") || playerShooting.GetProjectileState("Return"))  
        {
                 Debug.Log("Return Projectile To Player");
                playerProjectileBehavior.RemoveProjectile();
                playerStateManager.ChangePlayerState("Moving");
        }
    }
}
