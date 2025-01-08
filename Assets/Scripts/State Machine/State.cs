using UnityEngine;

public class State {
    protected FSM fsm;

    public State(FSM fsm){
        this.fsm = fsm;
    }

    public virtual void Enter(){

    }

    public virtual void Exit(){

    }

    public virtual void Update(){

    }

    public virtual void FixedUpdate(){
        
    }


}
