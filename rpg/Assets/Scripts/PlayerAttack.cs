using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, IObservable
{
    //singleton instance
    public static PlayerAttack singleton { get; private set; }
    
    private bool isAttackinging = false;
    private bool IsAttackinging
    {
        get => isAttackinging;
        set
        {
            if (isAttackinging != value)
            {
                isAttackinging = value;
                if (value)
                {
                    NotifyAttacking();
                    StartCoroutine(ReturnToIdleState());
                }
                else
                {
                    NotifyIdle();
                }
            }
        }
    }

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
        IsAttackinging = true;
    }

    private IEnumerator ReturnToIdleState()
    {
        yield return new WaitForSeconds(1);
        IsAttackinging = false;
    }
    
    public void Subscribe(IObserver observer)
    {
        PlayerMovementActions += observer.ChangeAnimation;
    }
    public void Unsubscribe(IObserver observer)
    {
        PlayerMovementActions -= observer.ChangeAnimation;
    }

    private void NotifyAttacking()
    {
        PlayerMovementActions?.Invoke(EPlayerState.ATTACK);
    }
    private void NotifyIdle()
    {
        PlayerMovementActions?.Invoke(EPlayerState.IDLE);
    }
}
