using UnityEngine;

public class Enemy_BattleState : Enemy_GroundedState
{
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Enter Battle State!");
    }
}
