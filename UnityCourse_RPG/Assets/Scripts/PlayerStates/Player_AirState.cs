using UnityEngine;

// 점프 상태시, 이동을 위한 상태 추가
public class Player_AirState : EntityState
{
    public Player_AirState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if(player.moveInput.x != 0)
        {
            // player.inAirMoveMultiplier : 공중 이동 속도 제어
            player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.inAirMoveMultiplier), rb.linearVelocityY);
        }

        if(input.Player.Attack.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.jumpAttackState);
        }
    }
}
