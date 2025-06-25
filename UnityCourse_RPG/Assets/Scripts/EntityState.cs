using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator animator;
    protected Rigidbody2D rb;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;   // player stateMachine
        this.animBoolName = animBoolName;

        animator = player.animator;
        rb = player.rb;
    }

    public virtual void Enter()
    {
        // evertime state will be chaned, enter will be called
        animator.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        // we going to run logic of the state here
        // Debug.Log("I run update of " + stateName);
    }

    public virtual void Exit()
    {
        // this will be called, everytime we exit state and change to new one
        animator.SetBool(animBoolName, false);
    }
}
