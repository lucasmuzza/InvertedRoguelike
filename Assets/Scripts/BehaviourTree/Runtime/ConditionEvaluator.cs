using System;
using System.Collections.Generic;
using UnityEngine;

public class ConditionEvaluator{
    public Dictionary<Type,Func<Condition,Blackboard,float>> conditionWeightEvaluators = new Dictionary<Type, Func<Condition, Blackboard, float>>();

    // The construct where it'll initialize and register the conditions to be evaluated 
    // Need to add the new condition to this construct
    public ConditionEvaluator(){
        RegisterCondition<DistanceChecker>((condition, blackboard) => ((DistanceChecker)condition).CalculateWeight(blackboard)); 
        RegisterCondition<HealthChecker>((condition, blackboard) => ((HealthChecker)condition).CalculateWeight(blackboard)); 
        RegisterCondition<VisibleChecker>((condition, blackboard) => ((VisibleChecker)condition).CalculateWeight(blackboard)); 
    }


    /// <summary>
    /// Registers a custom weight evaluation function for a specific condition type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="evaluator"></param'>
    public void RegisterCondition<T>(Func<Condition,Blackboard,float> evaluator){
        conditionWeightEvaluators[typeof(T)] = evaluator;
    }

    /// <summary>
    /// Evaluates the weight of a specific condition
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="blackboard"></param>
    /// <returns></returns>
    public float EvaluateCondition(Condition condition, Blackboard blackboard){
        if(conditionWeightEvaluators.TryGetValue(condition.GetType(), out Func<Condition,Blackboard,float> evaluator)){
            return evaluator(condition,blackboard);
        }

        return condition.CalculateWeight(blackboard);
    }

    /// <summary>
    /// Evaluates a list of conditions and returns the total weight
    /// </summary>
    /// <param name="conditions"></param>
    /// <param name="blackboard"></param>
    /// <returns></returns>
    public float EvaluateConditions(List<Condition> conditions, Blackboard blackboard){
        float totalWeight = 0;

        foreach(Condition condition in conditions){
            totalWeight += EvaluateCondition(condition,blackboard);
            Debug.Log("The total weight for the condition: " + condition + ", is: " + totalWeight + ".The agent is: " + blackboard.agent.name);
        }

        return totalWeight;
    }
}