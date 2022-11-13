using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public Animator Animator { get; private set; }

    [Tooltip("Layer where player can stand and restore jump&dash")]
    public LayerMask PlatformsLayer;
    public float Damage;
    public float TimeBetweenAttack;

    [Header("Attack range properties")]
    public Transform AttackTriggerPosition;
    public float AttackTriggerRadius;
    public LayerMask EnemyLayer;

    public float Speed;
    public float DashDistance;
    public bool DashReady = true;
    public float JumpHeight;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        StateMachine = new PlayerStateMachine(new Idle(this));
    }

    private void Update()
    {
        StateMachine.CurrentState.StateUpdate();
        StateMachine.CurrentState.TransitionCheck();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
}
