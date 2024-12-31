using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosition : ActionNode
{
    public float speed = 5;
    public float stoppingDistance = 0.1f;
    public bool updateRotation = true;
    public float acceleration = 40.0f;
    public float tolerance = 1.0f;

    protected override void OnStart() {
        
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
            return State.Running;
        }

}
