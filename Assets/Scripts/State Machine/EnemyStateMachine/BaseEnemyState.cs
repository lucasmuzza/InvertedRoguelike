using UnityEngine;

public class BaseEnemyState : EnemyState
{
    protected bool _hasBeenHit = false;

    public BaseEnemyState(EnemyStateMachine fsm, EnemyAIAgent enemyAIAgent, Animator animator, string animatorBool, AICombatSystem aiCombatSystem) : base(fsm, enemyAIAgent, animator, animatorBool, aiCombatSystem){
    
    }

    public override void DoChecks(){
        base.DoChecks();
    }

    public override void Enter(){
        base.Enter();
    }

    public override void Exit(){
        base.Exit();
    }

    public override void Update(){
        base.Update();

        if(fsm.currentState == fsm.GetState((int)EnemyStatesEnum.HURT) && !_hasBeenHit)
        {
            animator.SetTrigger("Hurt");
            _hasBeenHit = true;
        }

        // if(enemyAIAgent.aiHealthSystem.health.GetHealth() <= 0)
        // {
        //     fsm.SetCurrentState(fsm.GetState((int)EnemyStatesEnum.DIE));
        //     //_enemyAIAgent._navMeshAgent.isStopped = true;
        // }
    }

    public override void FixedUpdate(){
        base.FixedUpdate();
    }
}
