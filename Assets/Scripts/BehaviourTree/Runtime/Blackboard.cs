using System;
using UnityEngine;

[Serializable]
public class Blackboard {
    
    public GameObject agent;
    public GameObject target;
    public State currentState;
    public float currentAttackDistance;
}
