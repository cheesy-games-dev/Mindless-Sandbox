using UltEvents;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    public const float universalHealth = 100;
    public float maxHealth = universalHealth;
    public float startingHealth = universalHealth;
    public float currentHealth;
    public UltEvent<DamageInfo, float> OnDamaged = new();
    public UltEvent OnDeath = new();
    public virtual void Damage(DamageInfo info)
    {
        currentHealth -= info.Damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        DamageCallback(info);
        if (currentHealth <= 0)
        {
            DeathCallback();
        }
    }

    protected virtual void DamageCallback(DamageInfo info)
    {
        OnDamaged.Invoke(info, currentHealth);
    }

    protected virtual void DeathCallback()
    {
        OnDeath.Invoke();
    }

    void Start()
    {
        startingHealth = maxHealth <= startingHealth ? maxHealth : startingHealth;
        currentHealth = startingHealth;
    }
}
