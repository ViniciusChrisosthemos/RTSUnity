using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HealthController
{
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }

    public event Action<float, float> OnHealthChanded;

    public HealthController(float maxHealth, Action<float, float> onHealthChanged)
    {
        CurrentHealth = MaxHealth = maxHealth;

        OnHealthChanded += onHealthChanged;
    }

    public void TakeDamage(float value)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - value, 0f);

        OnHealthChanded?.Invoke(CurrentHealth, -value);
    }

    public void TakeHeal(float value)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + value, MaxHealth);

        OnHealthChanded?.Invoke(CurrentHealth, value);
    }

    public bool IsAlive() => CurrentHealth > 0f;
}
