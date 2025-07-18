using System;
using UnityEngine;

public class Enemy_BattleState : Enemy_GroundedState
{
    private Transform player;
    private float lastTimeWasInBattle;

    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if(player == null)
            player = enemy.CheckPlayerDetected().transform;

        if(ShouldRetreat() == true)
        {
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -GetDirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(GetDirectionToPlayer());
        }
    }

    public override void Update()
    {
        base.Update();

        if (enemy.CheckPlayerDetected() == true)
            UpdateBattleTimer();

        if(BattleTimeIsOver())
            stateMachine.ChangeState(enemy.idleState);

        if ( CheckWithinAttackRange() == true && enemy.CheckPlayerDetected() == true )
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            enemy.SetVelocity(enemy.battleMoveSpeed * GetDirectionToPlayer(), rb.linearVelocityY);
        }
    }

    private void UpdateBattleTimer() => lastTimeWasInBattle = Time.time;

    private bool BattleTimeIsOver() => Time.time > lastTimeWasInBattle + enemy.battleTimeDuration;

    private bool CheckWithinAttackRange() => GetDistanceToPlayer() < enemy.attackDistance;

    private bool ShouldRetreat() => GetDistanceToPlayer() < enemy.minRetreatDistance;

    private float GetDistanceToPlayer()
    {
        if( player == null)
            return float.MaxValue;

        return MathF.Abs(player.position.x - enemy.transform.position.x);
    }

    private int GetDirectionToPlayer()
    {
        if (player == null)
            return 0;

        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
