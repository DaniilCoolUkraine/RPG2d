using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _playerSprite;
    
    private BoxCollider2D _collider;
    [SerializeField] private LayerMask _platformsLayer;
    
    private Vector2 _moveDirection = Vector2.zero;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashCooldown;
    private float currentDashCooldown;
    private bool _dashReady = true;

    private PlayerInput _playerControls;
    private InputAction _move;
    private InputAction _fire;
    private InputAction _jump;
    private InputAction _dash;
    
    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _playerSprite = gameObject.GetComponent<SpriteRenderer>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
        _playerControls = new PlayerInput();
    }
    
    private void OnEnable()
    {
        _move = _playerControls.Player.Move;
        _move.Enable();

        _fire = _playerControls.Player.Fire;
        _fire.Enable();
        _fire.performed += Fire;

        _jump = _playerControls.Player.Jump;
        _jump.Enable();
        _jump.performed += Jump;

        _dash = _playerControls.Player.Dash;
        _dash.Enable();
        _dash.performed += Dash;
    }

    private void OnDisable()
    {
        _move.Disable();
        _fire.Disable();
        _jump.Disable();
        _dash.Disable();
    }

    private void Update()
    {
        _moveDirection = _move.ReadValue<Vector2>();
        
        if (_moveDirection.x > 0)
            _playerSprite.flipX = false;
        if (_moveDirection.x < 0)
            _playerSprite.flipX = true;

        if (currentDashCooldown >= 0)
        {
            currentDashCooldown -= Time.deltaTime;
        }
        else
        {
            _dashReady = true;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_moveDirection.x * speed, _rigidbody2D.velocity.y);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("fire");
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            Vector2 jump = new Vector2(0, jumpHeight);
            _rigidbody2D.AddForce(jump, ForceMode2D.Impulse);
        }
    }
    private bool IsGrounded()
    {
        var bounds = _collider.bounds;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, .1f, _platformsLayer);
        return raycastHit2D.collider != null;
    }

    private void Dash(InputAction.CallbackContext context)
    {
        Vector2 dash;
        if (_playerSprite.flipX)
            dash = new Vector2(-dashDistance, 0);
        else
            dash = new Vector2(dashDistance, 0);
        
        if (_dashReady)
        {
            _rigidbody2D.AddForce(dash);
            
            currentDashCooldown = dashCooldown;
            _dashReady = false;
        }
    }
}
