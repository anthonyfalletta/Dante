using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    //Abilities Variables
    bool _isMovePressed;
    bool _isDashPressed;
    bool _isShootPressed;
    bool _isAttackPressed;
    bool _isSpecialPressed;
    Vector2 _movementInputValue;
    Vector2 _lastMovementInputValue;
    //bool _isSpecialActive;

    public bool IsMovePressed {get{return _isMovePressed;} set{_isMovePressed = value;}}
    public bool IsDashPressed {get{return _isDashPressed;} set{_isDashPressed = value;}}
    public bool IsShootPressed {get{return _isShootPressed;} set{_isShootPressed = value;}}
    public bool IsAttackPressed {get{return _isAttackPressed;} set{_isAttackPressed = value;}}
    public bool IsSpecialPressed {get{return _isSpecialPressed;} set{_isSpecialPressed = value;}}

    public Vector2 MovementInputValue {get{return _movementInputValue;} set{_movementInputValue = value;}}
    public Vector2 LastMovementInputValue {get{return _lastMovementInputValue;} set{_lastMovementInputValue = value;}}

    private void Start() {
        LastMovementInputValue = new Vector2(0,-1);
    }
    
    public void OnMoveButton(InputAction.CallbackContext context)
    {
        _movementInputValue = context.ReadValue<Vector2>();
        _isMovePressed = _movementInputValue.magnitude != 0;

        if (_movementInputValue != Vector2.zero)
        {
            _lastMovementInputValue = _movementInputValue;
        }
    }

    public void OnDashButton(InputAction.CallbackContext context)
    {
        _isDashPressed = context.ReadValueAsButton();
        //Debug.Log("DASH Button Is Pressed");
    }
    public void OnShootButton(InputAction.CallbackContext context)
    {
        _isShootPressed = context.ReadValueAsButton();
        //Debug.Log("SHOOT Button Is Pressed");
    }
    public void OnAttackButton(InputAction.CallbackContext context)
    {
        _isAttackPressed = context.ReadValueAsButton();
        //Debug.Log("ATTACK Button Is Pressed");
    }
    public void OnSpecialButton(InputAction.CallbackContext context)
    {
        _isSpecialPressed = context.ReadValueAsButton();
        //Debug.Log("ATTACK Button Is Pressed");
    }
}
