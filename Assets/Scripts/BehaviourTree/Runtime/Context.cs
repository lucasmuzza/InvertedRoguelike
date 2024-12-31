using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// The context is a shared object every node has access to.
// Commonly used components and subsytems should be stored here
public class Context {
    public GameObject target;

    public GameObject aiAgent;
    public EnemyStatusHandler enemyStatusHandler;
    public EnemyHealthHandler enemyHealthHandler;
    public EnemyAttackHandler enemyAttackHandler;
    public EnemyMovementHandler enemyMovementHandler;
    public Animator animator;
    public Rigidbody2D rb;
    public BoxCollider boxCollider;

    // Add other game specific systems here

    public static Context CreateFromGameObject(GameObject gameObject) {
        // Fetch all commonly used components
        Context context = new Context();
        context.aiAgent = gameObject;

        context.enemyAttackHandler = gameObject.GetComponent<EnemyAttackHandler>();
        context.enemyHealthHandler = gameObject.GetComponent<EnemyHealthHandler>();
        context.enemyStatusHandler = gameObject.GetComponent<EnemyStatusHandler>();
        context.enemyMovementHandler = gameObject.GetComponent<EnemyMovementHandler>();

        context.animator = gameObject.GetComponent<Animator>();
        context.rb = gameObject.GetComponent<Rigidbody2D>();
        context.boxCollider = gameObject.GetComponent<BoxCollider>();
               

        return context;
    }
}

