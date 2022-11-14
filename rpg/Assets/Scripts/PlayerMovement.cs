using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IObservable
{
    //singleton instance
    public static PlayerMovement Singleton { get; private set; }

    //player gameobject components
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _playerSprite;
    private BoxCollider2D _collider;

    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float dashDistance;
                     private bool _dashReady = true;
    [Space(10)]
    [Tooltip("Layer where player can stand and restore jump&dash")]
    [SerializeField] private LayerMask[] platformsLayer;

    [SerializeField] private GameObject attackTriggerPosition;

    //player input class and its instances to store and read input from different devices
    private PlayerInput _playerMovementControls;
    private InputAction _move;
    private InputAction _jump;
    private InputAction _dash;
    
    //variable to move player 
    private Vector2 _moveDirection = Vector2.zero;
    private Vector2 MoveDirection
    {
        get => _moveDirection;
        set
        {
            if (_moveDirection != value)
            {
                _moveDirection = value;
                if(value == Vector2.zero)
                {
                    Notify(EPlayerState.IDLE);
                }
                else
                {
                    Notify(EPlayerState.RUNNING);
                }
            }
        }
    }
    
    //variable to player jump 
    private bool _isJumping;
    private bool IsJumping
    {
        get => _isJumping;
        set
        {
            if (_isJumping != value)
            {
                _isJumping = value;
                if (value)
                    Notify(EPlayerState.JUMPING);
            }
        }
    }
    
    //delegate and event to notify observers moving actions
    private delegate void PlayerMovementActionsHandler(EPlayerState state);
    private event PlayerMovementActionsHandler PlayerMovementActions;
    
    private void Awake()
    {
        //singleton logic
        if (!Singleton)
        {
            Singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        //observer logic
        Subscribe(gameObject.GetComponent<AnimationManager>());
        
        //getting components from player gameobject
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _playerSprite = gameObject.GetComponent<SpriteRenderer>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
        
        //initializing player input
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
        MoveDirection = _move.ReadValue<Vector2>();
        
        ChangeFlipState();
      
        ChangeDashStateToReady();
    }
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(MoveDirection.x * speed, _rigidbody2D.velocity.y);
    }

    #region playerInputActions
    private void Jump(InputAction.CallbackContext context)
    {
        if (CheckGrounded())
        {
            Vector2 jump = new Vector2(0, jumpHeight);
            _rigidbody2D.AddForce(jump, ForceMode2D.Impulse);
            IsJumping = true;
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
    
    //flip player facing its moving direction
    private void ChangeFlipState()
    {
        if (MoveDirection.x > 0)
        {
            _playerSprite.flipX = false;
            attackTriggerPosition.transform.localPosition = Vector3.right;
        }
        if (MoveDirection.x < 0)
        {
            _playerSprite.flipX = true;
            attackTriggerPosition.transform.localPosition = Vector3.left;
        }
    }
    private void ChangeDashStateToReady()
    {
        if (CheckGrounded())
            _dashReady = true;
    }
    
    private bool CheckGrounded()
    {
        var bounds = _collider.bounds;
        RaycastHit2D raycastHit2D = new RaycastHit2D();
        foreach (LayerMask layerMask in platformsLayer)
        {
            raycastHit2D = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, .1f, layerMask);   
        }

        if (raycastHit2D.collider != null)
        {
            IsJumping = false;
            return true;
        }
        IsJumping = true;
        return false;
    }

    public void Subscribe(IObserver observer)
    {
        PlayerMovementActions += observer.ChangeAnimation;
    }
    public void Unsubscribe(IObserver observer)
    {
        PlayerMovementActions -= observer.ChangeAnimation;
    }
    public void Notify(EPlayerState state)
    {
        PlayerMovementActions?.Invoke(state);
    }
}
