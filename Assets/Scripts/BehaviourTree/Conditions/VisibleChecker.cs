using UnityEngine;

public class VisibleChecker : Condition {
    public float detectionRadius = 2f;
    private Collider2D hit;
    public override bool IsTrue()
    {

        if (blackboard.agent == null || blackboard.target == null)
        {
            Debug.LogError("Agent or Target is not assigned in the Blackboard.");
            return false;
        }

        hit = Physics2D.OverlapCircle(blackboard.agent.transform.position,detectionRadius);
        
       if(hit.gameObject != null) 
            return true;

       return false;
    }
}
