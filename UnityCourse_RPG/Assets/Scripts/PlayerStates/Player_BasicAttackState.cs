using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    public float attackVelocityTimer;

    private const int FirstComboIndex = 0;
    private int comboIndex = FirstComboIndex;
    private int comboLimit = 0;
    private float lastTimeAttacked = 0.0f;

    private bool comboAttackQueued = false;

    private int attackDir;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        comboLimit = player.attackVelocity.Length - 1;
    }

    public override void Enter()
    {
        base.Enter();

        comboAttackQueued = false;
        ResetComboIndexIfNeeded();

        attackDir = player.moveInput.x != 0 ? (int)player.moveInput.x : (int)player.transform.right.x;

        animator.SetInteger("BasicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if( input.Player.Attack.WasPressedThisFrame() )
        {
            QueueNextAttack();
        }

        if(triggerCalled)
            HandleStateExit();
    }

    public override void Exit()
    {
        base.Exit();

        lastTimeAttacked = Time.time;
        comboIndex++;
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued == true)
        {
            animator.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
        {
            comboAttackQueued = true;
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

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;

        // 공격할때 움직이는 것처럼 보임
        player.SetVelocity(player.attackVelocity[comboIndex].x * player.transform.right.x * attackDir, player.attackVelocity[comboIndex].y);
    }

    private void ResetComboIndexIfNeeded()
    {
        if (player.comboResetTime + lastTimeAttacked < Time.time)
            comboIndex = FirstComboIndex;

        if (comboIndex > comboLimit)
            comboIndex = FirstComboIndex;
    }
}
