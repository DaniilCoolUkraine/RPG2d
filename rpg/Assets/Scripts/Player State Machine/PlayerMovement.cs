using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //player gameobject components
    //Running, Jump, Dash
    /*private Rigidbody2D _rigidbody2D;*/

    //Jump and Dash
    /*private BoxCollider2D _collider;*/

    //Running 
    /*[SerializeField] private float speed;*/

    //Jump
    /*[SerializeField] private float jumpHeight;*/

    //Dash
    /*[SerializeField] private float dashDistance;
                     private bool _dashReady = true;*/
    
    //Dash and Jump
    /*[Space(10)]
    [Tooltip("Layer where player can stand and restore jump&dash")]
    [SerializeField] private LayerMask platformsLayer;*/

    //player input class and its instances to store and read input from different devices
    //Running, Jump, Dash
    /*private PlayerInput _playerMovementControls;*/
    //Running
    /*private InputAction _move;
    //Jump
    private InputAction _jump;
    //Dash
    private InputAction _dash;*/
    
    //variable to move player 
    //Running
    /*private Vector2 _moveDirection = Vector2.zero;
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
                    //Notify(EPlayerState.IDLE);
                }
                else
                {
                    //Notify(EPlayerState.RUNNING);
                }
            }
        }
    }*/
    
    //variable to player jump 
    //Jump and Dash
    /*private bool _isJumping;
    private bool IsJumping
    {
        get => _isJumping;
        set
        {
            if (_isJumping != value)
            {
                _isJumping = value;
                //if (value) Notify(EPlayerState.JUMPING);
            }
        }
    }*/
    
    //delegate and event to notify observers moving actions
    /*private delegate void PlayerMovementActionsHandler(EPlayerState state);
    private event PlayerMovementActionsHandler PlayerMovementActions;*/
    
    private void Awake()
    {
        //observer logic
        //Subscribe(gameObject.GetComponent<AnimationManager>());
        
        //getting components from player gameobject
        /*_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();*/
        /*_collider = gameObject.GetComponent<BoxCollider2D>();*/
        
        //initializing player input
        //Running, Jump, Dash
        /*_playerMovementControls = new PlayerInput();*/
    }

    #region WORKING
    /*private void OnEnable()
    {
        //Running
        *//*_move = _playerMovementControls.Player.Move;
        _move.Enable();*//*

        //Jump
        _jump = _playerMovementControls.Player.Jump;
        _jump.Enable();
        _jump.performed += Jump;

        //Dash
        _dash = _playerMovementControls.Player.Dash;
        _dash.Enable();
        _dash.performed += Dash;
    }

    private void OnDisable()
    {
        //Running
        *//*_move.Disable();*//*
        //Jump
        _jump.Disable();
        //Dash
        _dash.Disable();
    }

    private void Update()
    {
        //Running
        *//*MoveDirection = _move.ReadValue<Vector2>();

        //Running
        ChangeFlipState();*//*

        //Dash
        ChangeDashStateToReady();
    }

    private void FixedUpdate()
    {
        //Running
        *//*_rigidbody2D.velocity = new Vector2(MoveDirection.x * speed, _rigidbody2D.velocity.y);*//*
    }

    #region playerInputActions
    //Jump
    private void Jump(InputAction.CallbackContext context)
    {
        if (CheckGrounded())
        {
            Vector2 jump = new Vector2(0, jumpHeight);
            _rigidbody2D.AddForce(jump, ForceMode2D.Impulse);
            IsJumping = true;
        }
    }

    //Dash
    private void Dash(InputAction.CallbackContext context)
    {
        Vector2 dash = new Vector2(dashDistance * Mathf.Sign(transform.localScale.x), 0);

        if (_dashReady)
        {
            _rigidbody2D.AddForce(dash);

            _dashReady = false;
        }
    }

    #endregion

    //Running
    //flip player facing its moving direction
    *//*private void ChangeFlipState()
    {
        transform.localScale = new Vector3(1 * Mathf.Sign(MoveDirection.x), 1, 1);
    }*//*

    //Dash
    private void ChangeDashStateToReady()
    {
        if (CheckGrounded())
            _dashReady = true;
    }*/
    #endregion

    //Jump and Dash
    /*private bool CheckGrounded()
    {
        var bounds = _collider.bounds;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, .1f, platformsLayer);

        if (raycastHit2D.collider != null)
        {
            IsJumping = false;
            return true;
        }
        IsJumping = true;
        return false;
    }*/

    /*public void Subscribe(IObserver observer)
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
    }*/
}
