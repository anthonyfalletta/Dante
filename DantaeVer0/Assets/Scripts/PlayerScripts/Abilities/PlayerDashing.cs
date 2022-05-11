using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashing : MonoBehaviour
{
    PlayerStateManager playerStateManager;
    [SerializeField] private LayerMask dashLayerMask;
    Vector2 dashDir;
    float dashSpeed;
    Rigidbody2D rb;

    PlayerMovement playerMovement;

    void Awake()
    {
        playerStateManager = GetComponent<PlayerStateManager>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Start is called before the first frame update
    void Start()
    {  
        dashSpeed = 100f;

    }

    // Update is called once per frame
    void Update()
    {
        FindMovementDirection();
    }

   

    private void FixedUpdate() {
        PerformDash();
    }

     private void FindMovementDirection()
    {
        dashDir = playerMovement.GetMovementValue().normalized;
    
    }

    private void DashSpeedSlowdown()
    {
        dashSpeed -= dashSpeed * 5f * Time.deltaTime;

        if(dashSpeed < 20f)
        {
            playerStateManager.ChangePlayerState("Moving");
            dashSpeed = 100f;
        }
    }

    public void OnDash(InputAction.CallbackContext value)
    {
        Debug.Log("Dashing System:" + value.phase);
        if (value.performed)
        {
            playerStateManager.ChangePlayerState("Dashing");
        }   
    }

    public void PerformDash()
    {
        if (playerStateManager.CheckPlayerState("Dashing"))
        {
            Debug.Log("Dash");

            /* Vector3 dashPosition = (transform.position + new Vector3(dashDir.x,dashDir.y, 0) * 15f * Time.deltaTime);
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, new Vector2(dashDir.x,dashDir.y), 15f, dashLayerMask);

            if (raycastHit2D.collider != null)
            {
                dashPosition = raycastHit2D.point;

            }

            rb.MovePosition(dashPosition); */

            rb.velocity = dashDir * dashSpeed * Time.deltaTime;
            DashSpeedSlowdown();
        }
        
    }
}
