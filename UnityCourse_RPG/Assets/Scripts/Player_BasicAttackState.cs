using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    public float attackVelocityTimer;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        GenerateAttackVelocity();
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

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        // 공격 도중 이동 불가
        if (attackVelocityTimer < 0)
        {
            player.SetVelocity(0, rb.linearVelocityY);
        }
    }

    private void GenerateAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;

        // 공격할때 움직이는 것처럼 보임
        player.SetVelocity(player.attackVelocity.x * player.transform.right.x, player.attackVelocity.y);
    }
}
