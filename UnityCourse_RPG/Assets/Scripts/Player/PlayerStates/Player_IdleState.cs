using UnityEngine;

public class Player_IdleState : Player_GroundState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, rb.linearVelocityY);
    }

    public override void Update()
    {
        base.Update();

        if( player.moveInput.x == player.transform.right.x && player.wallDetected )
        {
            return;
        }

        if( player.moveInput.x != 0 )
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}
