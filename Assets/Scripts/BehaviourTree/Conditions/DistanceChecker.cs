using UnityEngine;

public class DistanceChecker : Condition {
    public float weightWhenClose = 1.0f;
    public float weightWhenFar = 0.0f;

    public override bool IsTrue(){
        if (blackboard.agent == null || blackboard.target == null){
            Debug.LogError("Agent or Target is not assigned in the Blackboard.");
            return false;
        }

        float distance = Vector2.Distance(blackboard.agent.transform.position, blackboard.target.transform.position);
        return distance <= blackboard.currentAttackDistance;
    }

    // Add an additional method to calculate the weight based on distance
    public override float CalculateWeight(Blackboard blackboard){
        if (blackboard.agent == null || blackboard.target == null){
            return 0f;
        }

        float distance = Vector2.Distance(blackboard.agent.transform.position, blackboard.target.transform.position);
        if (distance <= blackboard.currentAttackDistance){
            return weightWhenClose;
        }
        else{
            return weightWhenFar;
        }
    }
}
