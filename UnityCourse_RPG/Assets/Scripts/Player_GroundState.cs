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

        if(input.Player.Jump.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
