using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    PlayerStateManager playerStateManager;
    PlayerProjectileBehavior playerProjectileBehavior;

    PlayerMovement playerMovement;
    public enum ShotState {None,Charging,Firing}
    public enum ProjectileState {Held, MovingForward, MovingBack, Free, Return}
    public ShotState shotState;
    public ProjectileState projectileState;
    Vector2 shootingPosition;
    float shootingDistance;
    RaycastHit2D raycastHit2D;
    Vector3 raycastPosition;
    public LayerMask playerLayerMask;
    
    private void Awake() {
        playerStateManager = GetComponent<PlayerStateManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerProjectileBehavior = GetComponent<PlayerProjectileBehavior>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shotState = ShotState.None;
        projectileState = ProjectileState.Held;
        shootingPosition = Vector2.zero;
        shootingDistance = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() {
        PerformShot();
        raycastPosition =  new Vector2(transform.position.x,transform.position.y) + (shootingPosition * shootingDistance);
        
        if (playerStateManager.CheckPlayerState("Shooting") == false)
        {
            //playerProjectileBehavior.RemoveProjectile();
        }
    }

    public void OnShoot(InputAction.CallbackContext value)
    {
        Debug.Log("Shooting System:" + value.phase);
        if(value.performed)
        {
           StartCharge();
        }     
        if (value.canceled)
        {
            EndCharge();
        }
    }

    private void PerformShot()
    {
        ChargingShot();
    }
    
    private void StartCharge()
    {
        if (projectileState == ProjectileState.Held)
        {
            shootingDistance = 0;
            playerProjectileBehavior.InstantiateProjectile();
            playerStateManager.ChangePlayerState("Shooting");       
        }
        else if(projectileState == ProjectileState.Free)
        {
            playerStateManager.ChangePlayerState("Shooting");
            projectileState = ProjectileState.Return;
        }
    }

    private void ChargingShot()
    {
        if (playerStateManager.CheckPlayerState("Shooting") == true && projectileState == ProjectileState.Held)
        {
            Debug.Log("Charging Shot");

            if (shootingDistance <= 2.0f)
            {
                shootingDistance += 1.25f * Time.deltaTime;
            }  

            RaycastingShootingPostion();
            playerProjectileBehavior.ProjectileSphereMovement();

        }
        else if (playerStateManager.CheckPlayerState("Shooting") == true && projectileState == ProjectileState.Return)
        {
            playerProjectileBehavior.SetPlayerTarget(gameObject.transform.position);
        }
        //Need to be able to switch back to Shoot once projectile is done moving
        /*
        else if (playerStateManager.CheckPlayerState("Shooting") == true && projectileState == ProjectileState.Free)
        {
            projectileState = ProjectileState.Return;
        }
        */
    }

    private void EndCharge()
    {
        if (playerStateManager.CheckPlayerState("Shooting") == true && projectileState == ProjectileState.Held)
        {
            //playerProjectileBehavior.RemoveProjectile();
            Debug.Log("Fire");
            //playerProjectileBehavior.ProjectilePath(); 
            
            playerStateManager.ChangePlayerState("Moving");
            //playerProjectileBehavior.ProjectilePath(raycastPosition);
            
            shootingDistance = 0;

            if (Vector3.Distance(gameObject.transform.position, raycastPosition) > 0.5f)
            {      
                projectileState = ProjectileState.MovingForward;  
                playerProjectileBehavior.SetTarget(raycastPosition);  
                playerProjectileBehavior.SetPlayerTarget(gameObject.transform.position);
            } 
            else
            {
                playerProjectileBehavior.RemoveProjectile();
            }       
        }
        else if (playerStateManager.CheckPlayerState("Shooting") == true && projectileState == ProjectileState.Return)
        {
            playerStateManager.ChangePlayerState("Moving");
            projectileState = ProjectileState.Free;
        }
    }

    private void RaycastingShootingPostion()
    {
            if (playerMovement.GetMovementValue() != Vector2.zero)
            {
                shootingPosition = playerMovement.GetMovementValue().normalized;
            }

            raycastHit2D = Physics2D.Raycast(transform.position, shootingPosition,shootingDistance, playerLayerMask);
            Debug.DrawRay(transform.position, shootingPosition*shootingDistance);

            if (raycastHit2D.collider == true)
            {
                Debug.Log("Contact");
                //shootingDistance = raycastHit2D.distance;                       
            }
            else 
            {
                Debug.Log("No Contact");
            }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(raycastPosition, 0.1f);
    }

   public bool GetProjectileState(string state)
   {
       switch(state)
       {
            case "Held":
                if (projectileState == ProjectileState.Held)
                    return true;
                else
                    return false;
            case "MovingForward":
                if (projectileState == ProjectileState.MovingForward)
                    return true;
                else
                    return false;
            case "MovingBack":
                if (projectileState == ProjectileState.MovingBack)
                    return true;
                else
                    return false;
            case "Free":
                if (projectileState == ProjectileState.Free)
                    return true;
                else
                    return false;
            case "Return":
                if (projectileState == ProjectileState.Return)
                    return true;
                else
                    return false;
            default:
            Debug.LogError("Proper Value not based to CheckPlayerState Script");
            return false;
       }
   }

   public void SetProjectileState(string state)
   {
       switch(state){
            case "Held":
                projectileState = ProjectileState.Held;
                break;
            case "MovingForward":
                projectileState = ProjectileState.MovingForward;
                break;
            case "MovingBack":
            projectileState = ProjectileState.MovingBack;
            break;
            case "Free":
                projectileState = ProjectileState.Free;
                break;
            case "Return":
                projectileState = ProjectileState.Return;
                break;
            default:
                Debug.LogError("Proper Value not based to ChangeProjectileState Script");
                break;
            
        }
   }
}
