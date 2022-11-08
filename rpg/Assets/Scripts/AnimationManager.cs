using UnityEngine;

public class AnimationManager : MonoBehaviour, IObserver
{
    public static AnimationManager singleton { get; private set; }

    [SerializeField] private Animator _animator;

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
    }
    
    public void ChangeAnimation(EPlayerState state)
    {
        switch (state)
        {
            case EPlayerState.IDLE:
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isAttacking", false);
                //Debug.Log("Idle");
                break;
            case EPlayerState.ATTACK:
                _animator.SetBool("isAttacking", true);
                _animator.SetBool("isRunning", false);
                Debug.Log("Attack");
                break;
            case EPlayerState.RUNNING: 
                _animator.SetBool("isRunning", true);
                _animator.SetBool("isAttacking", false);
                // Debug.Log("Run");
                break;
        }
    }
}
