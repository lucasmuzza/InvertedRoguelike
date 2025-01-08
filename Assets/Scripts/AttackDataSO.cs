using UnityEngine;

public enum StatusEffect
{
    None, Stunned, Slowed, Paralyzed, Burning, Bleeding, Poisoned, Knockback, Pull
}
[CreateAssetMenu()]
public class AttackDataSO : ScriptableObject
{
    public string attackName;
  
    [Space(10)]
    
    [Header("Attack Config")]
    public bool isBasicAttack;
    public bool isSpecialAttack;
    public bool isCombo;
    public LayerMask attackLayerMask;
    public AnimatorOverrideController animationOverrider;
    
    [Space(10)]
    
    [Header("Status Effect")]
    public bool hasStatusEffect;
    public StatusEffect statusEffect;
    
    [Space(10)]
    
    [Header("Attack Variables")]
    public float baseDamage;
    public float attackSpeed;
    public float attackCooldown;
    public float attackHitProbability;
    public float[] damageReducedOverTime;
    
    [Space(10)]
    
    [Header("Status Variables")]
    public float statusEffectDuration;
    public float statusEffectHitProbability;
    public float[] statusDamageOverTime;

    public GameObject attackPrefab;
}
