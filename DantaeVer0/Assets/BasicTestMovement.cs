using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicTestMovement : MonoBehaviour
{
    Rigidbody2D _playerRb;

    //Abilities Variables
    bool _isMovePressed;
    bool _isDashPressed;
    bool _isShootPressed;
    bool _isAttackPressed;
    bool _isSpecialPressed;
    //Vector2 _movementInputValue;
    //Vector2 _lastMovementInputValue;
    //bool _isSpecialActive;

    //public bool IsMovePressed {get{return _isMovePressed;} set{_isMovePressed = value;}}
    //public bool IsDashPressed {get{return _isDashPressed;} set{_isDashPressed = value;}}
    //public bool IsShootPressed {get{return _isShootPressed;} set{_isShootPressed = value;}}
    //public bool IsAttackPressed {get{return _isAttackPressed;} set{_isAttackPressed = value;}}
    //public bool IsSpecialPressed {get{return _isSpecialPressed;} set{_isSpecialPressed = value;}}

    //public Vector2 MovementInputValue {get{return _movementInputValue;} set{_movementInputValue = value;}}
    //public Vector2 LastMovementInputValue {get{return _lastMovementInputValue;} set{_lastMovementInputValue = value;}}

    public PlayerAnimationController animator;
    public PlayerInputController input;

    bool bMove;
    bool bStop;

    private void Awake() {
        _playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayerAnimationController>();
        input = GetComponent<PlayerInputController>();
    }
    
    private void Start() {
       
    }
    
    private void Update() {
        if (input.IsMovePressed)
        {
            //Debug.Log("Moving");
            bMove = true;
        }
        else{
            //Debug.Log("Fixed");
            bMove = false;
        }
      
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move(){
        if(bMove){
        animator.EnableMoveAnimation();
        _playerRb.MovePosition(_playerRb.position + input.MovementInputValue.normalized * 2f * Time.fixedDeltaTime);
         }
        else if(!bMove){
            //_playerRb.velocity = Vector2.zero;
            animator.DisableMoveAnimation();
        }
    }



}
