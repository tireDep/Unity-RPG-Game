using UnityEngine;

public class Player_DashState : PlayerState
{
    private float originalGravityScale;

    private int dashDir;

    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0.0f;

        dashDir = player.moveInput.x != 0 ? (int)player.moveInput.x : (int)player.transform.right.x;
    }

    public override void Update()
    {
        base.Update();

        CancelDashIfNeeded();
        player.SetVelocity(player.dashSpeed * player.transform.right.x, 0);

        if(stateTimer < 0.0f )
        {
            if (player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, 0);
        rb.gravityScale = originalGravityScale;
    }

    private void CancelDashIfNeeded()
    {
        if (player.wallDetected == false)
        {
            return;
        }
        
        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
