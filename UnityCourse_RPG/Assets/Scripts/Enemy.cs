using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;

    [Header("Movement Details")]
    public float idleTime = 2;
    public float moveSpeed = 1.5f;
    [Range(0, 2)]
    public float moveAnimSpeedMultiplier = 1;

    [Header("Player Detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10.0f;

    public RaycastHit2D CheckPlayerDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * transform.right.x, playerCheckDistance, whatIsPlayer | whatIsGround);
        if( hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            // 감지 못함 or 플레이어가 아닌 경우
            return default;
        }

        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (transform.right.x * playerCheckDistance), playerCheck.position.y));
    }
}
