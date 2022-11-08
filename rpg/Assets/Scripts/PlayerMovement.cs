using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IObservable
{
    //singleton instance
    public static PlayerMovement singleton { get; private set; }

    //player gameobject components
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _playerSprite;
    private BoxCollider2D _collider;
    
    //variable to move player and store input 
    private Vector2 _moveDirection = Vector2.zero;

    private bool isRunning = false;
    private bool IsRunning
    {
        get => isRunning;
        set
        {
            isRunning = value;
            if (value)
                NotifyRunning();
            else
                NotifyIdle();
        }
    }

    private delegate void PlayerMovementActionsHandler(EPlayerState state);
    private event PlayerMovementActionsHandler PlayerMovementActions;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float dashDistance;
                     private bool _dashReady = true;

    [Space(10)]
    [Tooltip("Layer where player can stand and restore jump&dash")]
    [SerializeField] private LayerMask platformsLayer;
    
    //player input class and its properties to store and read input from different devices
    private PlayerInput _playerMovementControls;
    private InputAction _move;
    private InputAction _jump;
    private InputAction _dash;
    
    private void Awake()
    {
        //singleton logic
        if (!singleton)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        Subscribe(gameObject.GetComponent<AnimationManager>());
        
        //getting components from player gameobject
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _playerSprite = gameObject.GetComponent<SpriteRenderer>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
        
        _playerMovementControls = new PlayerInput();
    }
    
    private void OnEnable()
    {
        _move = _playerMovementControls.Player.Move;
        _move.Enable();

        _jump = _playerMovementControls.Player.Jump;
        _jump.Enable();
        _jump.performed += Jump;

        _dash = _playerMovementControls.Player.Dash;
        _dash.Enable();
        _dash.performed += Dash;
    }

    private void OnDisable()
    {
        _move.Disable();
        _jump.Disable();
        _dash.Disable();
    }

    private void Update()
    {
        _moveDirection = _move.ReadValue<Vector2>();
        
        ChangeFlipState();
        ChangeRunState();
        
        ChangeDashStateToReady();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_moveDirection.x * speed, _rigidbody2D.velocity.y);
    }

    //flip player facing its moving direction
    private void ChangeFlipState()
    {
        if (_moveDirection.x > 0)
            _playerSprite.flipX = false;
        if (_moveDirection.x < 0)
            _playerSprite.flipX = true;
    }

    private void ChangeRunState()
    {
        if (_moveDirection.x != 0)
            IsRunning = true;
        if (_moveDirection.x == 0)
            IsRunning = false;
    }
    
    private void ChangeDashStateToReady()
    {
        if (IsGrounded())
            _dashReady = true;
    }
    
    private bool IsGrounded()
    {
        var bounds = _collider.bounds;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, .1f, platformsLayer);
        
        if (raycastHit2D.collider != null) 
            return true;
        return false;
    }

    #region playerInputActions

    private void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            Vector2 jump = new Vector2(0, jumpHeight);
            _rigidbody2D.AddForce(jump, ForceMode2D.Impulse);
        }
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
            
            _dashReady = false;
        }
    }
    
    #endregion

    public void Subscribe(IObserver observer)
    {
        PlayerMovementActions += observer.ChangeAnimation;
    }
    public void Unsubscribe(IObserver observer)
    {
        PlayerMovementActions -= observer.ChangeAnimation;
    }

    private void NotifyRunning()
    {
        PlayerMovementActions?.Invoke(EPlayerState.RUNNING);
    }
    private void NotifyIdle()
    {
        PlayerMovementActions?.Invoke(EPlayerState.IDLE);
    }
}
