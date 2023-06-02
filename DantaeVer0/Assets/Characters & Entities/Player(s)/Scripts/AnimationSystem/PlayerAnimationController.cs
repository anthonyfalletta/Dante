using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator _animator;
    PlayerInputController _input;
    public Animator Animator {get{return _animator;} set{_animator = value;}}
    public PlayerInputController Input {get{return _input;} set{_input = value;}}

    int _isMovingHash;
    int _isDashingHash;

    public int IsMovingHash{get{return _isMovingHash;} set{_isMovingHash = value;}}
    public int IsDashingHash{get{return _isDashingHash;} set{_isDashingHash = value;}}

    private void Awake() {
        _animator = this.GetComponent<Animator>();
        _input = this.GetComponent<PlayerInputController>();
        AnimationHashing();
    }

    void Start()
    {
       
    }

    void Update()
    {
        HandleMoveAnimation();
    }

    void AnimationHashing()
    {
        _isMovingHash = Animator.StringToHash("isMoving");
        _isDashingHash = Animator.StringToHash("isDashing");
    }

    public void EnableMoveAnimation(){
        Animator.SetBool(IsMovingHash, true);
    }

    public void DisableMoveAnimation(){
        Animator.SetBool(IsMovingHash, false);
    }

    public void EnableDashAnimation(){
        Animator.SetBool(IsDashingHash, true);
    }

    public void DisableDashAnimation(){
        Animator.SetBool(IsDashingHash, false);
    }

    public void HandleMoveAnimation()
    {
        _animator.SetFloat("Horizontal", Input.MovementInputValue.x);
        _animator.SetFloat("Vertical", Input.MovementInputValue.y);
        _animator.SetFloat("Magnitude", Input.MovementInputValue.sqrMagnitude);
        _animator.SetFloat("LastHorizontal", Input.LastMovementInputValue.x);
        _animator.SetFloat("LastVertical", Input.LastMovementInputValue.y);       
    }
}
