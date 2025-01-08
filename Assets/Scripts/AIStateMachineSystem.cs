using UnityEngine;

public class AIStateMachineSystem
{
    public Context aiAgentContext;
    public Blackboard aiAgentBlackboard;
    public EnemyAIAgent enemyAIAgent;
    public EnemyStateMachine enemyStateMachine;

    public void Initialize(EnemyAIAgent enemyAIAgent){
        
        this.enemyAIAgent = enemyAIAgent;

        aiAgentContext = enemyAIAgent.context;

        aiAgentBlackboard = enemyAIAgent.blackboard;

        enemyStateMachine = new EnemyStateMachine();
        
        // Add all the states of the Ai agent here:
    }

    public void Tick(){
        aiAgentBlackboard.currentState = enemyStateMachine.currentState;
    }

    public void SetState(EnemyStatesEnum enemyStatesEnum){
        enemyStateMachine.SetCurrentState(enemyStateMachine.GetState((int)enemyStatesEnum));
    }
}
