using System.Collections.Generic;
using UnityEngine;

public class FSM {
    public State currentState;
    protected Dictionary<int,State> states = new Dictionary<int, State>();


    public FSM(){

    }

    public void Add(int key, State state){
        states[key] = state;
    }

    public void SetCurrentState(State state){
        if(currentState != null){
            currentState.Exit();
        }

        currentState = state;

        if(currentState != null){
            currentState.Enter();
        }
    }

    public State GetState(int key){
        return states[key];
    }

    public void Update(){
        if(currentState != null){
            currentState.Update();
        }
    }

    public void FixedUpdate(){
        if(currentState != null){
            currentState.FixedUpdate();
        }
    }
}
