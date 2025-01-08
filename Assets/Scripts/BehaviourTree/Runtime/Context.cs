using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// The context is a shared object every node has access to.
// Commonly used components and subsytems should be stored here
public class Context {
    public GameObject target;

    public GameObject aiAgentObj;
    public EnemyAIAgent enemyAIAgent;
    public AICombatSystem aiAgentCombatSystem;
    
    public Animator animator;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;

    // Add other game specific systems here

    public static Context CreateFromGameObject(GameObject gameObject) {
        // Fetch all commonly used components
        Context context = new Context();
        context.aiAgentObj = gameObject;

        context.enemyAIAgent = gameObject.GetComponent<EnemyAIAgent>();
        context.aiAgentCombatSystem = new AICombatSystem();
        context.aiAgentCombatSystem.Initialize(context.enemyAIAgent,context.enemyAIAgent.basicAttacks,context.enemyAIAgent.specialAttacks);
        
        context.animator = gameObject.GetComponent<Animator>();
        context.rb = gameObject.GetComponent<Rigidbody2D>();
        context.boxCollider = gameObject.GetComponent<BoxCollider2D>();
               

        return context;
    }
}

