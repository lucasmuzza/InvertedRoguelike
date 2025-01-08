using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class AttackCooldownChecker : Condition
{
    public bool isSpecialAttack;

    public float weightSpecialAttack = 1f;
    public float weightBasicAttack = 0.5f;
    public override bool IsTrue()
    {
        if(isSpecialAttack && blackboard.agent != null){

            foreach(bool isSpecialAttackOnCooldown in blackboard.agent.GetComponent<AICombatSystem>().specialAttacksActiveCooldowns){
                if(!isSpecialAttackOnCooldown){
                    return true;
                }
            }
        }

        else if(!isSpecialAttack){
            return true;
        }

        return false;
    }

    public override float CalculateWeight(Blackboard blackboard){
        if (blackboard.agent == null || blackboard.target == null){
            return 0f;
        }

        if(isSpecialAttack && IsTrue()){
            return weightSpecialAttack;
        }
        else if(isSpecialAttack && !IsTrue()){
            return 0f;
        }
            

        if(!isSpecialAttack && IsTrue())
        {
            return weightBasicAttack;
        }

        return 0f;
    }
}
