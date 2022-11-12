using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerState<T> where T : PlayerStateData
{
    protected T Data { get; private set; }

    protected PlayerInput PlayerMovementControls { get; private set; }
    protected InputAction Action { get; set; }

    public PlayerState(T data)
    {
        Data = data;
    }

    //Start
    public virtual void Start()
    {
        PlayerMovementControls = new PlayerInput();
    }

    //OnEnable
    public abstract void Enter();

    public virtual void StateUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    //OnDisable
    public abstract void Exit();
}

public abstract class PlayerMovementState<T> : PlayerState<T> where T : PlayerStateData
{
    protected Rigidbody2D Rb { get; private set; }

    public PlayerMovementState(T data) : base(data)
    {
        
    }

    public override void Start()
    {
        base.Start();
        Rb = Data.Unit.GetComponent<Rigidbody2D>();
    }
}

public abstract class PlayerGroundedState<T> : PlayerMovementState<T> where T : PlayerGroundedStateData
{
    protected bool IsJumping { get; private set; }
    protected BoxCollider2D Collider;

    public PlayerGroundedState(T data) : base(data)
    {

    }

    public override void Start()
    {
        base.Start();
        Collider = Data.Unit.GetComponent<BoxCollider2D>();
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
