using System.Collections.Generic;
using UnityEngine;

public abstract class OptionNode: Node {
    [HideInInspector] public List<Node> children = new List<Node>();
    [SerializeReference] public List<Condition> conditions;
    
    protected int current;
    public bool requireAllConditions = true;

    protected override void OnStart(){
        foreach(Condition condition in conditions){
            condition.blackboard = blackboard;
        }
    }

    protected override void OnStop(){

    }

    protected override State OnUpdate(){
        if(requireAllConditions){
            foreach(Condition condition in conditions){
                if(condition.IsTrue()){
                    return State.Failure;
                }
            }
            
            for(int i = 0; i < children.Count; i++){
                current = i;
                var child = children[current];

                child.Update();
            }

            return State.Success;
        }

        else{
            foreach(Condition condition in conditions){
                if(condition.IsTrue()){
                    return State.Success;
                }
            }
        }

        return State.Running;
    }

    public override Node Clone(){
        OptionNode node = Instantiate(this);
        node.children = children.ConvertAll(c => c.Clone());
        return node;
    }
}