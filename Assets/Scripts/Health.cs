using UnityEngine;
using UnityEngine.Events;

public class Health {
    public float maxHealth;
    public float currentHealth;
    public UnityEvent onHealthChanged = new UnityEvent();


    public Health(float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public float GetCurrentHealth(){
        return currentHealth;
    }

    public float GetHealthPercent()    {
        return maxHealth > 0 ? currentHealth/maxHealth : 0f;
    }

    public void TakeDamage(float damageAmount){
        Debug.Log("Receiving damage, the amount of damage is: " + damageAmount);
        currentHealth -= damageAmount;
        if(currentHealth < 0) currentHealth = 0;
        onHealthChanged?.Invoke();
    }

    public void Heal(float healAmount){
        currentHealth += healAmount;
        if(currentHealth > maxHealth) currentHealth = maxHealth;
        onHealthChanged?.Invoke();
    }
}
