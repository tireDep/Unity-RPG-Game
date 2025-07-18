using System.Runtime.Serialization;
using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;

        rb = enemy.rb;
        animator = enemy.animator;
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();

        float battleAnimSpeedMultiplier = enemy.battleMoveSpeed / enemy.moveSpeed;
        animator.SetFloat("BattleAnimSpeedMultiplier", battleAnimSpeedMultiplier);
        animator.SetFloat("MoveAnimSpeedMultiplier", enemy.moveAnimSpeedMultiplier);
        animator.SetFloat("xVelocity", rb.linearVelocityX);
    }
}
