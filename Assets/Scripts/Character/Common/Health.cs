using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float maxHP;

    [SerializeField]
    private float currentHP;

    public event Action<Health> OnDeath;
    public event Action<float> OnDamageTaken;
    public event Action<float, float> OnHealthChanged;

    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public bool IsDead => currentHP <= 0f;

    public void Init(float hp)
    {
        maxHP = hp;
        ResetHealth();
    }

    public void ApplyStats(SurvivalStats stats)
    {
        SetMaxHP(stats.maxHP, false);
    }

    public void SetMaxHP(float newMaxHP, bool healToFull = true)
    {
        maxHP = newMaxHP;

        currentHP = healToFull ? maxHP : Mathf.Min(currentHP, maxHP);

        NotifyHealthChanged();
    }

    public void ResetHealth()
    {
        currentHP = maxHP;
        NotifyHealthChanged();
    }

    public void TakeDamage(float damage)
    {
        if (IsDead)
            return;

        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0f);

        OnDamageTaken?.Invoke(damage);
        NotifyHealthChanged();

        if (currentHP <= 0f)
            Die();
    }

    public void Heal(float amount)
    {
        if (IsDead)
            return;

        currentHP = Mathf.Min(currentHP + amount, maxHP);
        NotifyHealthChanged();
    }

    private void NotifyHealthChanged()
    {
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    private void Die()
    {
        OnDeath?.Invoke(this);
    }
}