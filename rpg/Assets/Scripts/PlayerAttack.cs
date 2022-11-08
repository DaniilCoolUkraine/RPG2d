using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, IObservable
{
    //singleton instance
    public static PlayerAttack singleton { get; private set; }
    
    private delegate void PlayerMovementActionsHandler(EPlayerState state);
    private event PlayerMovementActionsHandler PlayerMovementActions;
    
    private PlayerInput _playerMovementControls;
    private InputAction _fire;

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
        
        _playerMovementControls = new PlayerInput();
    }

    private void OnEnable()
    {
        _fire = _playerMovementControls.Player.Fire;
        _fire.Enable();
        _fire.performed += Fire;
    }

    private void OnDisable()
    {
        _fire.Disable();
    }
    
    private void Fire(InputAction.CallbackContext context)
    {
        NotifyAttacking();
    }

    public void Subscribe(IObserver observer)
    {
        PlayerMovementActions += observer.ChangeAnimation;
    }
    public void Unsubscribe(IObserver observer)
    {
        PlayerMovementActions -= observer.ChangeAnimation;
    }

    public void NotifyAttacking()
    {
        PlayerMovementActions?.Invoke(EPlayerState.ATTACK);
    }
}
