using System;

public interface IHealthProvider
{
    float CurrentHP { get; }
    float MaxHP { get; }

    event Action<float, float> OnHealthChanged;
    event Action OnDeath;
}