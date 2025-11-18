using Reflex.Attributes;
using UnityEngine;

public class FirstKit : Item
{
    [Inject] private readonly PoolItems _poolIFirstKit;

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Player _) && collider.TryGetComponent(out Health health))
        {
            _audioSource.Play();
            health.RestoreHealth(_strengthEffect);
            _poolIFirstKit.ReturnObject(this);
        }
    }
}
