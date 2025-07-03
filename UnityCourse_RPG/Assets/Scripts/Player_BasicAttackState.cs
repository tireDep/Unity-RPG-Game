using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    public float attackVelocityTimer;

    private const int FirstComboIndex = 0;
    private int comboIndex = FirstComboIndex;
    private int comboLimit = 3;
    private float lastTimeAttacked = 0.0f;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if( comboLimit != player.attackVelocity.Length)
        {
            Debug.Log("Player_BasicAttackState:: Combo Limit != AttackVelocity.Length");
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();

        ResetComboIndexIfNeeded();

        animator.SetInteger("BasicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if(triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        lastTimeAttacked = Time.time;
        comboIndex++;
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        // 공격 도중 이동 불가
        if (attackVelocityTimer < 0)
        {
            player.SetVelocity(0, rb.linearVelocityY);
        }
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;

        // 공격할때 움직이는 것처럼 보임
        player.SetVelocity(player.attackVelocity[comboIndex].x * player.transform.right.x, player.attackVelocity[comboIndex].y);
    }

    private void ResetComboIndexIfNeeded()
    {
        if (player.comboResetTime + lastTimeAttacked < Time.time)
            comboIndex = FirstComboIndex;

        if (comboIndex >= comboLimit)
            comboIndex = FirstComboIndex;
    }
}
