using UnityEngine;

public class EnemyState : State
{
    protected EnemyAIAgent enemyAIAgent;
    protected Animator animator;
    protected AICombatSystem aiCombatSystem;

    protected bool isAnimatorTrigger = false;
    protected string animatorBool;
    protected float startTime;
    public EnemyState(EnemyStateMachine fsm, EnemyAIAgent enemyAIAgent, Animator animator, string animatorBool, AICombatSystem aiCombatSystem) : base(fsm){
        this.enemyAIAgent = enemyAIAgent;
        this.animator = animator;
        this.animatorBool = animatorBool;
        this.aiCombatSystem = aiCombatSystem;
    }

    public override void Enter(){
        DoChecks();

        startTime = Time.time;

        if(!isAnimatorTrigger){
            animator.SetBool(animatorBool,true);
        }
        else{
            animator.SetTrigger(animatorBool);
        }

        base.Enter();
    }

    public override void Exit(){
        if(!isAnimatorTrigger){
            animator.SetBool(animatorBool,false);
        }
        else{
            animator.ResetTrigger(animatorBool);
        }

        base.Exit();
    }

    public override void Update() => base.Update();

    public override void FixedUpdate(){
        DoChecks();
        base.FixedUpdate();
    }

    public virtual void DoChecks(){ }
}
