using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    private Health _health;

    protected Health Health => _health ??= GetComponentInParent<Health>();

    private void OnEnable()
    {
        Health.HealthChanged += ChangeHealth;
    }

    private void OnDisable()
    {
        Health.HealthChanged -= ChangeHealth;
    }

    public abstract void ChangeHealth();
}
