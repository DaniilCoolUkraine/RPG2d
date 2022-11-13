using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    /*[SerializeField] private float damage;
    [SerializeField] private float timeBetweenAttack;
    [Header("Attack range properties")]
    [SerializeField] private Transform attackTriggerPosition;
    [SerializeField] private float attackTriggerRadius;
    [SerializeField] private LayerMask enemyLayer;*/


    //player input class and its instances to store and read input from different devices
    #region WORKING
    /*private float _currentTime;
    //Attack
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
                    //Notify(EPlayerState.ATTACK);
                    StartCoroutine(ReturnToIdleState());
                }
                else
                {
                    //Notify(EPlayerState.IDLE);
                }
            }
        }
    }

    //delegate and event to notify observers attacking actions
    private delegate void PlayerAttackActionsHandler(EPlayerState state);
    private event PlayerAttackActionsHandler PlayerAttackActions;

    private void Awake()
    {
        //observer logic
        //Subscribe(gameObject.GetComponent<AnimationManager>());

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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackTriggerPosition.position, attackTriggerRadius);
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

    private Collider2D GetEnemyToDamage()
    {
        Collider2D enemy = Physics2D.OverlapCircle(attackTriggerPosition.position, attackTriggerRadius, enemyLayer);

        return enemy;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (_canAttack)
        {
            IsAttacking = true;
            _canAttack = false;

            Collider2D enemy = GetEnemyToDamage();
            if (enemy != null)
                StartCoroutine(DamageEnemy(enemy));
        }
    }
    private IEnumerator DamageEnemy(Collider2D enemy)
    {
        yield return new WaitForSeconds(.3f);
        enemy.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
    }

    private IEnumerator ReturnToIdleState()
    {
        yield return new WaitForSeconds(1);
        IsAttacking = false;
    }*/
    #endregion

    /*public void Subscribe(IObserver observer)
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
    }*/
}
