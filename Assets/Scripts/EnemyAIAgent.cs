using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BehaviourTreeRunner))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAIAgent : MonoBehaviour
{
   [Header("Components")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;

    [Space(10)]

    [Header("AI Related Components")]
    public BehaviourTreeRunner behaviourTreeRunner;
    public Context context;
    public Blackboard blackboard;

    [Header("AI Combat")]
    public AICombatSystem aiCombatSystem;
    public List<AttackDataSO> basicAttacks;
    public List<AttackDataSO> specialAttacks;
    private void Awake()
    {
        behaviourTreeRunner = GetComponent<BehaviourTreeRunner>();
        
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        // aiCombatSystem = new AICombatSystem();
        // aiCombatSystem.Initialize(this,basicAttacks,specialAttacks);
    }

    // Update is called once per frame
    void Update()
    {
        //aiCombatSystem.Tick();
    }
}
