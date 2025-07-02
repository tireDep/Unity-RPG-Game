using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator animator;
    protected Rigidbody2D rb;
    protected PlayerInputSet input;

    protected float stateTimer;

    protected bool triggerCalled;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;   // player stateMachine
        this.animBoolName = animBoolName;

        animator = player.animator;
        rb = player.rb;
        input = player.input;
    }

    public virtual void Enter()
    {
        // evertime state will be chaned, enter will be called
        animator.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        // we going to run logic of the state here
        animator.SetFloat("yVelocity", rb.linearVelocityY);

        if( input.Player.Dash.WasPressedThisFrame() && CanDash() )
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    public virtual void Exit()
    {
        // this will be called, everytime we exit state and change to new one
        animator.SetBool(animBoolName, false);
    }

    public void SetAnimationTrigger()
    {
        triggerCalled = true;
    }

    private bool CanDash()
    {
        if (player.wallDetected)
            return false;

        if (stateMachine.currentState == player.dashState)
            return false;

        return true;
    }
}
