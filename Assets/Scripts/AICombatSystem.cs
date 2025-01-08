using System.Collections.Generic;
using UnityEngine;

public class AICombatSystem
{
    public EnemyAIAgent enemyAIAgent;
    public AIStateMachineSystem aiStateMachineSystem;
    public LayerMask attackLayer;
    public Vector2 attackHitBoxSize;
    public Vector2 attackHitBoxPos;


    public List<AttackDataSO> basicAttacks;
    public List<AttackDataSO> specialAttacks;

    public int comboCounter;
    public int specialAttackIndex;

    public float currentAttackDamage;
    public float lastAttackTime;
    public float lastComboEnd;

    public bool[] specialAttacksActiveCooldowns;
    public float[] specialAttacksCooldownTimers;

    public void Initialize(EnemyAIAgent enemyAIAgent, List<AttackDataSO> basicAttacks, List<AttackDataSO> specialAttacks){
       
        this.enemyAIAgent = enemyAIAgent;
        this.basicAttacks = basicAttacks;
        this.specialAttacks = specialAttacks;

        specialAttacksActiveCooldowns = new bool[specialAttacks.Count];
        specialAttacksCooldownTimers = new float[specialAttacks.Count];
    }

    public void Tick(){

        UpdateCooldowns();
        EndAttack();
    }

    public void BasicAttack(){
        
        if(Time.time - lastComboEnd > 0.5f && comboCounter < basicAttacks.Count){
            enemyAIAgent.GetComponent<MonoBehaviour>().CancelInvoke(nameof(EndCombo));

            if(Time.time - lastAttackTime >= 0.6f){
                
                aiStateMachineSystem.SetState(EnemyStatesEnum.ATTACK);

                enemyAIAgent.animator.SetTrigger("Attack");

                enemyAIAgent.animator.runtimeAnimatorController = basicAttacks[comboCounter].animationOverrider;

                currentAttackDamage = basicAttacks[comboCounter].baseDamage;

                lastAttackTime = Time.time;

                if(comboCounter >= basicAttacks.Count){
                    comboCounter = 0;
                }
            }
        }
    }

    public void SpecialAttack(int index){
        
        specialAttackIndex = index;

        if(!specialAttacksActiveCooldowns[index]){

            aiStateMachineSystem.SetState(EnemyStatesEnum.ATTACK);

            enemyAIAgent.animator.SetTrigger("SpecialAttack");

            enemyAIAgent.animator.runtimeAnimatorController = specialAttacks[index].animationOverrider;

            currentAttackDamage = specialAttacks[index].baseDamage;
            StartCooldown(index);
            comboCounter = 0;
        }
    }

    public void PerformAttack(){

    }

    public void StartCooldown(int index){

        specialAttacksActiveCooldowns[index] = true;
        specialAttacksCooldownTimers[index] = specialAttacks[index].attackCooldown;
    }

    public void UpdateCooldowns(){

        for(int i = 0; i < specialAttacksCooldownTimers.Length; i++){

            if(specialAttacksActiveCooldowns[i]){

                specialAttacksCooldownTimers[i] -= Time.deltaTime;

                if(specialAttacksCooldownTimers[i] <= 0){

                    specialAttacksActiveCooldowns[i] = false;
                }
            }
        }
    }

    public void EndAttack(){

        if(enemyAIAgent.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && enemyAIAgent.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            enemyAIAgent.GetComponent<MonoBehaviour>().Invoke("EndCombo",1f);
        }

    }

    public void EndCombo(){

        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
