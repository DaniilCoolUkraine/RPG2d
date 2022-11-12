using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStateData", menuName = "PlayerStateData")]
public abstract class PlayerStateData : ScriptableObject
{
    public GameObject Unit => _unit;
    [SerializeField] private GameObject _unit;
}

[CreateAssetMenu(fileName = "NewPlayerGroundedStateData", menuName = "PlayerGroundedStateData")]
public abstract class PlayerGroundedStateData : PlayerStateData
{
    [Tooltip("Layer where player can stand and restore jump&dash")]
    public LayerMask PlatformsLayer;
}

[CreateAssetMenu(fileName = "NewPlayerStateData", menuName = "PlayerStateData/Attack State")]
public class PlayerAttackStateData : PlayerStateData
{
    public float Damage;
    public float TimeBetweenAttack;

    [Header("Attack range properties")]
    public Transform AttackTriggerPosition;
    public float AttackTriggerRadius;
    public LayerMask EnemyLayer;
}

[CreateAssetMenu(fileName = "NewPlayerStateData", menuName = "PlayerStateData/Running State")]
public class PlayerRunningStateData : PlayerStateData
{
    public float Speed;
}

[CreateAssetMenu(fileName = "NewPlayerStateData", menuName = "PlayerStateData/Dash State")]
public class PlayerDashStateData : PlayerGroundedStateData
{
    public float DashDistance;
    public bool DashReady = true;
}

[CreateAssetMenu(fileName = "NewPlayerStateData", menuName = "PlayerStateData/Jump State")]
public class PlayerJumpStateData : PlayerGroundedStateData
{
    public float JumpHeight;
}
