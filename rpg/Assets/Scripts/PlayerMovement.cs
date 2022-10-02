using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection = Vector2.zero;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;

    private PlayerInput _playerControls;
    private InputAction _move;
    private InputAction _fire;
    private InputAction _jump;

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
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
    }

    private void OnDisable()
    {
        _move.Disable();
        _fire.Disable();
        _jump.Disable();
    }

    private void Update()
    {
        _moveDirection = _move.ReadValue<Vector2>();
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
        if (_rigidbody2D.velocity.y == 0)
        {
            Vector2 jump = new Vector2(0, jumpHeight);
            _rigidbody2D.AddForce(jump, ForceMode2D.Impulse);
        }
    }
}
