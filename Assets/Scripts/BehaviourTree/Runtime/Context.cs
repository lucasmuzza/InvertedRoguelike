using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// The context is a shared object every node has access to.
// Commonly used components and subsytems should be stored here
public class Context {
    public GameObject aiAgent;
    public GameObject target;
    public Transform transform;
    public Animator animator;
    public Rigidbody physics;
    public NavMeshAgent agent;
    public SphereCollider sphereCollider;
    public BoxCollider boxCollider;
    public CapsuleCollider capsuleCollider;
    public CharacterController characterController;
    // Add other game specific systems here

    public static Context CreateFromGameObject(GameObject gameObject) {
        // Fetch all commonly used components
        Context context = new Context();
        context.aiAgent = gameObject;
        context.transform = gameObject.transform;
        context.animator = gameObject.GetComponent<Animator>();
        context.physics = gameObject.GetComponent<Rigidbody>();
        context.agent = gameObject.GetComponent<NavMeshAgent>();
        context.boxCollider = gameObject.GetComponent<BoxCollider>();        

        return context;
    }
}

