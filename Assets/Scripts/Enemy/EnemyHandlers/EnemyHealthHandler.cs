using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour {
    
    [SerializeField] private GameObject healthBar;
    public float maxHealth;
    public Health health {get;private set;}

    void Start(){
        health = new Health(maxHealth);
        Setup(health);
    }

    void Update(){
        UpdateHealthBar();
        if(health.currentHealth <= 0)
            Die();
    }

    public void Setup(Health health){
        this.health = health;
        health.onHealthChanged.AddListener(UpdateHealthBar);
    }

    public void UpdateHealthBar(){
        float healthPercent = Mathf.Clamp(health.GetHealthPercent(),0f,1f);
        healthBar.transform.localScale = new Vector3(healthPercent,1,1);
    }

    public void Hit(float damageAmount){
        health.TakeDamage(damageAmount);
    }

    public void Die(){

    }
}
