using System.Collections.Generic;
using UnityEngine;

public class SequenceReasoner : CompositeNode{
    private Node bestChild;
    private Node currentActiveChild;
    private float highestWeight;
    private ConditionEvaluator evaluator;

    protected override void OnStart() 
    {
        evaluator = new ConditionEvaluator();
        bestChild = null;
        currentActiveChild = null;
        highestWeight = float.MinValue;
    }

    protected override void OnStop() {
        // Abort the currently active child if it exists
        if (currentActiveChild != null) 
        {
            currentActiveChild.Abort();
            currentActiveChild = null;
        }
    }

    protected override State OnUpdate() {
        bestChild = null;
        highestWeight = float.MinValue;

        // Iterate through child nodes and find the child with the highest weight
        foreach (Node child in children) 
        {
            if (child is OptionNode optionNode) 
            {
                // Evaluate the weight of the option node's conditions
                float weight = evaluator.EvaluateConditions(optionNode.conditions, blackboard);

                if (weight > highestWeight) {
                    highestWeight = weight;
                    bestChild = child;
                }
            }
        }

        // Check if the best child is different from the currently active child
        if (bestChild != currentActiveChild) 
        {
            // Abort the current active child
            if (currentActiveChild != null) {
                currentActiveChild.Abort();
            }

            // Set the new best child as the active child
            currentActiveChild = bestChild;

            // Start the new active child by updating it's state
            if (currentActiveChild != null) 
            {
                currentActiveChild.Update();
            }
        }

        return State.Running;
    }
}

