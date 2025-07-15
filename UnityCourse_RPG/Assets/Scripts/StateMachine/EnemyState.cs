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

    public override void Update()
    {
        base.Update();

        animator.SetFloat("MoveAnimSpeedMultiplier", enemy.moveAnimSpeedMultiplier);
    }
}
