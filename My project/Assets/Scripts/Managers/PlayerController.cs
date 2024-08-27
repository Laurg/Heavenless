using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour

{
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;
    private Vector3 _mousePosition;
    private Vector3 _characterDirection;
    private bool _canDash = true;
    private bool _isDashing;

    [SerializeField] private float _dashingPower;
    [SerializeField] private float _dashingTime;
    [SerializeField] private float _dashingCooldown;
    [SerializeField] private TrailRenderer _tr;
    [SerializeField] private float _speed;

    public Weapon weapon;


    private void Awake()
    {
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
        _direction = new Vector3(_input.x, 0.0f,_input.y);
    }

    public void RotateCamera()
   {

       _mousePosition = Mouse.current.position.ReadValue();
       _mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(_mousePosition.x, _mousePosition.y, Camera.main.transform.position.y));
       _characterDirection = new Vector3(_mousePosition.x - transform.position.x, 0.0f, _mousePosition.z - transform.position.z);
       transform.forward = _characterDirection;

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
        Debug.Log("entró por aquí");
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
}
