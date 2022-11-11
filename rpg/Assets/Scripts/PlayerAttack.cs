using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, IObservable
{
    //singleton instance
    public static PlayerAttack Singleton { get; private set; }
    
    [SerializeField] private float damage;
    [SerializeField] private float timeBetweenAttack;
    [SerializeField] private float _currentTime;
    
    //player input class and its instances to store and read input from different devices
    private PlayerInput _playerAttackControls;
    private InputAction _fire;

    //variable to player attack
    private bool _canAttack = true;
    private bool _isAttacking;
    private bool IsAttacking
    {
        get => _isAttacking;
        set
        {
            if (_isAttacking != value)
            {
                _isAttacking = value;
                if (value)
                {
                    Notify(EPlayerState.ATTACK);
                    StartCoroutine(ReturnToIdleState());
                }
                else
                {
                    Notify(EPlayerState.IDLE);
                }
            }
        }
    }

    //delegate and event to notify observers attacking actions
    private delegate void PlayerAttackActionsHandler(EPlayerState state);
    private event PlayerAttackActionsHandler PlayerAttackActions;
    
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
        
        //initializing player input
        _playerAttackControls = new PlayerInput();
    }
    private void OnEnable()
    {
        _fire = _playerAttackControls.Player.Fire;
        _fire.Enable();
        _fire.performed += Fire;
    }
    private void OnDisable()
    {
        _fire.Disable();
    }
    private void Start()
    {
        _currentTime = timeBetweenAttack;
    }
    private void Update()
    {
        if (!_canAttack)
        {
            ChangeCanAttackState();
        }
    }

    private void ChangeCanAttackState()
    {
        if (_currentTime <= 0)
        {
            _canAttack = true;
            _currentTime = timeBetweenAttack;
        }
        else
            _currentTime -= Time.deltaTime;
    }
    
    private void Fire(InputAction.CallbackContext context)
    {
        if (_canAttack)
        {
            IsAttacking = true;
            _canAttack = false; 
        }
    }
    
    private IEnumerator ReturnToIdleState()
    {
        yield return new WaitForSeconds(1);
        IsAttacking = false;
    }
    
    public void Subscribe(IObserver observer)
    {
        PlayerAttackActions += observer.ChangeAnimation;
    }
    public void Unsubscribe(IObserver observer)
    {
        PlayerAttackActions -= observer.ChangeAnimation;
    }
    public void Notify(EPlayerState state)
    {
        PlayerAttackActions?.Invoke(state);
    }
}
