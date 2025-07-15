using UnityEngine;

public class Player_WallJumpState : PlayerState
{
    public Player_WallJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // if( ( player.moveInput.x >= 0 && player.transform.right.x > 0 )
        //     || (player.moveInput.x <= 0 && player.transform.right.x < 0)
        //     )
        // {
        //     player.SetVelocity(player.wallJumpForce.x * player.transform.right.x, player.wallJumpForce.y);
        // }
        // else
        // {
        //     player.SetVelocity(player.wallJumpForce.x * -player.transform.right.x, player.wallJumpForce.y);
        // }

        // 반대방향 점프
        player.SetVelocity(player.wallJumpForce.x * -player.transform.right.x, player.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();

        if( rb.linearVelocityY < 0 )
        {
            stateMachine.ChangeState(player.fallState);
        }

        if( player.wallDetected )
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
