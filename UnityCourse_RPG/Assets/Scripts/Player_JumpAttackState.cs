using UnityEngine;

public class Player_JumpAttackState : EntityState
{
    private bool touchedGround;
    public Player_JumpAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        touchedGround = false;
        player.SetVelocity(player.jumpAttackVelocity.x * player.transform.right.x, player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.groundDetected && touchedGround == false )
        {
            touchedGround = true;
            animator.SetTrigger("JumpAttackTrigger");
            player.SetVelocity(0, rb.linearVelocityY);
        }

        if( triggerCalled && player.groundDetected )
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
