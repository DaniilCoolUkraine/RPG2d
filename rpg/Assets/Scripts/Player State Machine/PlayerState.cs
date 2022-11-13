using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Attack
public abstract class PlayerState
{
    protected PlayerEntity Data { get; private set; }

    protected PlayerInput PlayerMovementControls { get; private set; }
    protected InputAction Action { get; set; }

    protected string IS_ATTACKING => "isAttacking";
    protected string IS_RUNNING => "isRunning";
    protected string IS_JUMPING => "isJumping";

    public PlayerState(PlayerEntity data)
    {
        Data = data;
    }

    //OnEnable
    public virtual void Enter()
    {
        PlayerMovementControls = new PlayerInput();
    }

    public virtual void StateUpdate()
    {

    }

    public virtual void TransitionCheck()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    //OnDisable
    public virtual void Exit()
    {

    }
}

//Running, Jump, and Dash
public abstract class PlayerMovementState : PlayerState
{
    protected Rigidbody2D Rb { get; private set; }

    public PlayerMovementState(PlayerEntity data) : base(data)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        Rb = Data.GetComponent<Rigidbody2D>();
    }
}

//Jump and Dash
public abstract class PlayerGroundedState : PlayerMovementState
{
    protected bool IsJumping { get; private set; }
    protected BoxCollider2D Collider;

    public PlayerGroundedState(PlayerEntity data) : base(data)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Collider = Data.GetComponent<BoxCollider2D>();
    }

    protected bool CheckGrounded()
    {
        var bounds = Collider.bounds;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, .1f, Data.PlatformsLayer);

        if (raycastHit2D.collider != null)
        {
            IsJumping = false;
            return true;
        }
        IsJumping = true;
        return false;
    }
}

public class Idle : PlayerState
{
    private Vector2 _moveDirection = Vector2.zero;

    public Idle(PlayerEntity data) : base(data)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Data.Animator.SetBool(IS_RUNNING, false);
        Data.Animator.SetBool(IS_ATTACKING, false);
        Data.Animator.SetBool(IS_JUMPING, false);

        Action = PlayerMovementControls.Player.Move;
        Action.Enable();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        _moveDirection = Action.ReadValue<Vector2>();
    }

    public override void TransitionCheck()
    {
        base.TransitionCheck();
        if (_moveDirection != Vector2.zero)
        {
            Data.StateMachine.ChangeState(new Running(Data));
        }
    }

    public override void Exit()
    {
        Action.Disable();
    }
}

public class Running : PlayerMovementState
{
    private Vector2 _moveDirection = Vector2.zero;

    public Running(PlayerEntity data) : base(data)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Data.Animator.SetBool(IS_RUNNING, true);
        Data.Animator.SetBool(IS_ATTACKING, false);
        Data.Animator.SetBool(IS_JUMPING, false);

        Action = PlayerMovementControls.Player.Move;
        Action.Enable();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        _moveDirection = Action.ReadValue<Vector2>();
        ChangeFlipState();
    }

    public override void TransitionCheck()
    {
        base.TransitionCheck();
        if (_moveDirection == Vector2.zero)
        {
            Data.StateMachine.ChangeState(new Idle(Data));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Rb.velocity = new Vector2(_moveDirection.x * Data.Speed, Rb.velocity.y);
    }

    public override void Exit()
    {
        Action.Disable();
    }

    private void ChangeFlipState()
    {
        Data.transform.localScale = new Vector3(1 * Mathf.Sign(_moveDirection.x), 1, 1);
    }
}
