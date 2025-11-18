using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private CreaterEffects createrEffects;

    public event Action HealthOver;
    public event Action HealthChanged;

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    private void Start()
    {
        SetHealth();

        createrEffects = GetComponent<CreaterEffects>();
    }

    public void SetHealth()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (!enabled) return;

        if (CurrentHealth > 0)
        {
            CurrentHealth -= damage;
            HealthChanged?.Invoke();

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        createrEffects.Show();
        HealthOver?.Invoke();
    }

    public void RestoreHealth(float health)
    {
        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth += health;

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        HealthChanged?.Invoke();
    }

    public void Init(float valueHealth, float maxHealth)
    {
        CurrentHealth = Mathf.Max(1, valueHealth);
        MaxHealth = maxHealth;
    }
}