using UnityEngine;

// Superstate
public class Player_GroundState : EntityState
{
    public Player_GroundState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if( rb.linearVelocityY < 0 && player.groundDetected == false)
        {
            // 낙하 상태 감지
            stateMachine.ChangeState(player.fallState);
        }

        if(input.Player.Jump.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
