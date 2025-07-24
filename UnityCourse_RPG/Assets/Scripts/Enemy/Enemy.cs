using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;

    [Header("Battle Details")]
    public float battleMoveSpeed = 3.0f;
    public float attackDistance = 2.0f;
    public float battleTimeDuration = 5.0f;
    public float minRetreatDistance = 1.0f;
    public Vector2 retreatVelocity;

    [Header("Movement Details")]
    public float idleTime = 2;
    public float moveSpeed = 1.5f;
    [Range(0, 2)]
    public float moveAnimSpeedMultiplier = 1;

    [Header("Player Detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10.0f;
    public Transform player { get; private set; }

    public void TryEnterBattleState(Transform player)
    {
        if (stateMachine.currentState == battleState || stateMachine.currentState == attackState)
            return;

        this.player = player;
        stateMachine.ChangeState(battleState);
    }

    public Transform GetPlayerReference()
    {
        if (player == null)
        {
            player = CheckPlayerDetected().transform;
        }

        return player;
    }

    public RaycastHit2D CheckPlayerDetected()
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

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (transform.right.x * attackDistance), playerCheck.position.y));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (transform.right.x * minRetreatDistance), playerCheck.position.y));
    }
}
