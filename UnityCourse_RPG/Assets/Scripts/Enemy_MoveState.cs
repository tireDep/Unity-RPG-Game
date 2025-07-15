using UnityEngine;

public class Enemy_MoveState : EnemyState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (enemy.groundDetected == false || enemy.wallDetected)
            enemy.Flip();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.transform.right.x, rb.linearVelocityY);

        if( enemy.groundDetected == false || enemy.wallDetected )
            stateMachine.ChangeState(enemy.idleState);
    }
}
