using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable,IHealthProvider
{
    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;

    private bool isDead;

    public event Action<Health> OnDeath;
    public event Action<float> OnDamageTaken;
    public event Action<float, float> OnHealthChanged;

    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public bool IsDead => isDead;

    public void Init(float hp)
    {
        maxHP = Mathf.Max(1f, hp);
        currentHP = maxHP;
        isDead = false;

        NotifyHealthChanged();
    }

    public void ApplyStats(SurvivalStats stats)
    {
        SetMaxHP(stats.maxHP, false);
    }

    public void SetMaxHP(float newMaxHP, bool healToFull = true)
    {
        maxHP = Mathf.Max(1f, newMaxHP);

        if (healToFull)
            currentHP = maxHP;
        else
            currentHP = Mathf.Min(currentHP, maxHP);

        NotifyHealthChanged();
    }

    public void ResetHealth()
    {
        currentHP = maxHP;
        isDead = false;

        NotifyHealthChanged();
    }

    public void TakeDamage(float damage, WeaponRuntime source)
    {
        if (isDead)
            return;

        if (source != null)
            source.Stats.totalDamage += damage;

        float finalDamage = Mathf.Max(0f, damage);
        currentHP -= finalDamage;
        currentHP = Mathf.Max(currentHP, 0f);

        OnDamageTaken?.Invoke(finalDamage);
        NotifyHealthChanged();

        if (currentHP <= 0f)
            Die();
    }

    public void Heal(float amount)
    {
        if (isDead)
            return;

        currentHP = Mathf.Min(currentHP + Mathf.Max(0f, amount), maxHP);

        NotifyHealthChanged();
    }

    public void Revive(float hp = 1f)
    {
        isDead = false;
        currentHP = Mathf.Clamp(hp, 0f, maxHP);

        NotifyHealthChanged();
    }

    private void NotifyHealthChanged()
    {
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        OnDeath?.Invoke(this);
    }
}