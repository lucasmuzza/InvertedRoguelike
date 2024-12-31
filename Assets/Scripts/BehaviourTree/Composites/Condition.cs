using System;
using UnityEngine;

[Serializable]
public abstract class Condition
{
    [HideInInspector] public Blackboard blackboard;
    [HideInInspector] public Context context;

    /// <summary>
    /// IsTrue checks if the implemented condition is true.
    /// </summary>
    /// <returns>The result of the condition.</returns>
    public abstract bool IsTrue();

    /// <summary>
    /// OnStart is called as a setup method for the Condition before it begins checking its logic.
    /// </summary>
    public virtual void OnStart()
    {
    
    }
    
    /// <summary>
    /// OnEnd is called as a cleanup method for the Condition after it completes its logic check.
    /// </summary>
    public virtual void OnEnd()
    {

    }

    /// <summary>
    /// Calculates the weight of the condition. Override this in derived classes for specific logic.
    /// </summary>
    public virtual float CalculateWeight(Blackboard blackboard) {
        return IsTrue() ? 1f : -1f; // Default weight calculation
    }

    public virtual void OnDrawGizmos() { }
}

