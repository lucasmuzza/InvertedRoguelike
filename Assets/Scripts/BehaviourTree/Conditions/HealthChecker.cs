using UnityEngine;

public class HealthChecker : Condition {

   public float weightLowHealth = 1.0f;
    public float weightFullHealth = 0.1f;
    public float weightMidHealth = 0.5f;

    public float lowHealthThreshold = 0.3f;
    public float midHealthThreshold = 0.7f;

    private float healthPercentage;

    public override bool IsTrue(){
        if (blackboard.agent == null){
            Debug.LogError("Agent is not assigned in the Blackboard");
            return false;
        }

        // Calculate the percentage of health
        healthPercentage = context.enemyHealthHandler.health.GetHealthPercent();

        if (healthPercentage <= lowHealthThreshold){
            return true; // Low health case
        }
        else if (healthPercentage > lowHealthThreshold && healthPercentage <= midHealthThreshold){
            return true; // Mid health case
        }
        else{
            return false; // Full health case
        }
    }

    public float CalculateHealthWeight(Blackboard blackboard){
        if (blackboard.agent == null){
            return 0f; // No agent found, return no weight
        }

        // Assign weights based on health percentage
        if (healthPercentage <= lowHealthThreshold){
            return weightLowHealth; 
        }
        else if (healthPercentage > lowHealthThreshold && healthPercentage <= midHealthThreshold){
            return weightMidHealth; 
        }
        else{
            return weightFullHealth;
        }
    }

}
