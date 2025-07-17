using System;
using UnityEngine;

public class Enemy_BattleState : Enemy_GroundedState
{
    private Transform player;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if(player == null)
            player = enemy.CheckPlayerDetection().transform;
    }

    public override void Update()
    {
        base.Update();

        if( CheckWithinAttackRange() == true )
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            enemy.SetVelocity(enemy.battleMoveSpeed * GetDirectionToPlayer(), rb.linearVelocityY);
        }
    }

    private bool CheckWithinAttackRange() => GetDistanceToPlayer() < enemy.attackDistance;
    // {
    //     return GetDistanceToPlayer() < enemy.attackDistance;
    // }

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
