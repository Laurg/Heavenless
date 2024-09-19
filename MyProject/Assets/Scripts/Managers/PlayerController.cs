using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour

{
    #region
    public static Transform instance;

    /*private void Awake()
    {
        instance = this.transform;
    }*/
    #endregion
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;
    private Vector3 _mousePosition;
    private Vector3 _characterDirection;
    private bool _canDash = true;
    private bool _isDashing;
    private Camera mainCamera;
    private Animator animator;

    [SerializeField] private float _dashingPower;
    [SerializeField] private float _dashingTime;
    [SerializeField] private float _dashingCooldown;
    [SerializeField] private TrailRenderer _tr;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask groundMask;

    public Weapon weapon;

    private void Start()
    {
        // Cache the camera, Camera.main is an expensive operation.
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
       animator.SetBool("IsMoving", false);
    }

    private void Awake()
    {
        instance = this.transform;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _characterController.Move(_direction* _speed *Time.deltaTime);
        RotateCamera();
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        bool isMoving = _input.sqrMagnitude > 0 || _direction.sqrMagnitude > 0;
        animator.SetBool("IsMoving", isMoving);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetBool("IsMoving", true);
            _input = context.ReadValue<Vector2>();
            _direction = new Vector3(_input.x, -1.0f, _input.y);
        }
        else
        {
            animator.SetBool("IsMoving", false);
            _input = Vector2.zero;
            _direction = Vector3.zero;
        }
    }

    public void RotateCamera()
   {
        var (success, position) = GetMousePosition();
        if (success)
        {
            
            var direction = position - transform.position;

            
            direction.y = 0;

            
            transform.forward = direction;
        }
    }


    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && _canDash)
        {
            StartCoroutine(Dash());
        }
    }

    public IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        _characterController.Move(_direction * _dashingPower * Time.deltaTime);
        _tr.emitting = true;
        yield return new WaitForSeconds(_dashingTime);
        _tr.emitting = false;
        _isDashing = false;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weapon.Fire();
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }
}
