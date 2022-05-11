using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacking : MonoBehaviour
{
    public GameObject attackObject;
    GameObject playerObject;

    PlayerStateManager playerStateManager;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        playerMovement = playerObject.GetComponent<PlayerMovement>();
        playerStateManager = playerObject.GetComponent<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            //Attacking Process
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        //Change State Attack Then Quickly Change Back
        //Instiante Attack GameObject
        //wait
        //Destroy Attack GameObject
        float angle = Mathf.Atan2(playerMovement.GetMovementValue().normalized.y,playerMovement.GetMovementValue().normalized.x) * Mathf.Rad2Deg;
        Quaternion attackRotation = Quaternion.Euler(new Vector3(0,0,angle));
            //projectile.transform.position = transform.position + (new Vector3(playerMovement.GetMovementValue().x,playerMovement.GetMovementValue().y,0).normalized * 0.1f);
        Vector3 attackPosition = transform.position + (new Vector3(playerMovement.GetMovementValue().x,playerMovement.GetMovementValue().y,0).normalized * 0.1f);
        StartCoroutine(AttackSwipe(attackPosition, attackRotation));
        
    }

    IEnumerator AttackSwipe(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Performing Attack");
        playerStateManager.ChangePlayerState("Attacking");
        
            
        Instantiate(attackObject, position,rotation);
        yield return new WaitForSeconds(2f);
    }
}
