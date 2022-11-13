using UnityEngine;

public class AnimationManager : MonoBehaviour
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
                _animator.SetBool("isJumping", false);
                break;
            case EPlayerState.ATTACK:
                _animator.SetBool("isAttacking", true);
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isJumping", false);
                break;
            case EPlayerState.RUNNING: 
                _animator.SetBool("isRunning", true);
                _animator.SetBool("isAttacking", false);
                _animator.SetBool("isJumping", false);
                break;
            case EPlayerState.JUMPING: 
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isAttacking", false);
                _animator.SetBool("isJumping", true);
                break;
        }
    }
}
