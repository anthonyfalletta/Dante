using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerAbilitiesAnimations playerAbilitiesAnimations;
    PlayerStateManager playerStateManager;
    Rigidbody2D rb;
    Vector2 movementValue;

    void Awake() {
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAbilitiesAnimations = GetComponent<PlayerAbilitiesAnimations>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        PerformMovement();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        movementValue = value.ReadValue<Vector2>();
    }

    public void PerformMovement()
    {
        if (playerStateManager.CheckPlayerState("Moving"))
        {
            rb.velocity = movementValue.normalized * 25f * Time.deltaTime;
            playerAbilitiesAnimations.HandleMoveAnimation(movementValue.normalized);
        }
    }

    public Vector2 GetMovementValue()
    {
        return movementValue;
    }

    public void SetMovementValueZero()
    {
        rb.velocity = Vector2.zero * Time.deltaTime;
    }
}
