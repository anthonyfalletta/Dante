using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerMovement playerMovement;
    public PlayerProjectileBehavior playerProjectileBehavior;
    public enum PlayerState {Moving,Dashing,Shooting,Attacking}
    public PlayerState playerState;
    

    private void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        playerProjectileBehavior = GetComponent<PlayerProjectileBehavior>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.Moving;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePlayerState(string state)
    {
        switch(state){
            case "Moving":
                playerState = PlayerState.Moving;
                break;
            case "Dashing":
                //if player state shooting, remove projectile
                if (playerState == PlayerState.Shooting)
                {
                    playerProjectileBehavior.RemoveProjectile();
                }
                playerState = PlayerState.Dashing;
                break;
            case "Shooting":
                playerState = PlayerState.Shooting;
                playerMovement.SetMovementValueZero();
                break;
            case "Attacking":
                playerState = PlayerState.Attacking;

                break; 
            default:
                Debug.LogError("Proper Value not based to ChangePlayerState Script");
                break;
            
        }
    }

    public bool CheckPlayerState(string state)
    {
       switch(state)
       {
           case "Moving":
                if (playerState == PlayerState.Moving)
                    return true;
                else
                    return false;
            case "Dashing":
                if (playerState == PlayerState.Dashing)
                    return true;
                else
                    return false;
            case "Shooting":
                if (playerState == PlayerState.Shooting)
                    return true;
                else
                    return false;
            case "Attacking":
                if (playerState == PlayerState.Attacking)
                    return true;
                else
                    return false;
            default:
                    Debug.LogError("Proper Value not based to CheckPlayerState Script");
                    return false; 
       }       
    }



}
