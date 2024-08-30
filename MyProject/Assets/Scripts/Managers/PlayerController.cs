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
    }
    public void Move(InputAction.CallbackContext context)
    {
        
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, -1.0f,_input.y);
    }

    public void RotateCamera()
   {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;

            // You might want to delete this line.
            // Ignore the height difference.
            direction.y = 0;

            // Make the transform look in the direction.
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
