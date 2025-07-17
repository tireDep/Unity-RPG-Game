using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;

    [Header("Movement Details")]
    public float idleTime = 2;
    public float moveSpeed = 1.5f;
    [Range(0, 2)]
    public float moveAnimSpeedMultiplier = 1;
}
