using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesAnimations : MonoBehaviour
{
    Animator animator;
    float lastMovementX;
    float lastMovementY;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        lastMovementX = 0;
        lastMovementY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleMoveAnimation(Vector2 movement)
    {
        

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Magnitude", movement.sqrMagnitude);
            animator.SetFloat("LastHorizontal", lastMovementX);
            animator.SetFloat("LastVertical", lastMovementY);


        if (movement.sqrMagnitude > 0)
        {
            lastMovementX = movement.x;
            lastMovementY = movement.y;
        }
        
    }
}
