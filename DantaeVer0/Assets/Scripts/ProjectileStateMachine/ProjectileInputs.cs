using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileInputs:MonoBehaviour
{
    GameObject _playerGO;
    Rigidbody2D _playerRb;
    Vector2 _movementInputValue;
    bool _isShootPressed;
    bool _isSpecialPressed;

    GameObject _projectilePrefab;
    
    bool projectileCreated;


    public Vector2 MovementInputValue {get{return _movementInputValue;}}
    public bool IsShootPressed {get{return _isShootPressed;} set{_isShootPressed = value;}}
    public bool IsSpecialPressed {get{return _isSpecialPressed;} set{_isSpecialPressed = value;}}

    private void Awake() {
        //Setup Player Variables
        _playerGO = GameObject.Find("Player");
        _playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }
    private void Start() {
        projectileCreated = false;
    }

    public void OnMoveButton(InputAction.CallbackContext context)
    {
        _movementInputValue = context.ReadValue<Vector2>();
    }

    public void OnShootButton(InputAction.CallbackContext context)
    {      
        _isShootPressed = context.ReadValueAsButton();  
        if (_isShootPressed == true && projectileCreated == false)       
        {
            projectileCreated = true;
        }
        else if (_isShootPressed == false && projectileCreated == true)
        {
            projectileCreated = false;
        }
    }

    public void OnSpecialButton(InputAction.CallbackContext context)
    {
        _isSpecialPressed = context.ReadValueAsButton();
    }
}
