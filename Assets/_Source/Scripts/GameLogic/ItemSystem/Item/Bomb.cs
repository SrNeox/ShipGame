using Reflex.Attributes;
using UnityEngine;

public class Bomb : Item
{
    [Inject] private PoolItems _poolItems;

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Player _) && collider.TryGetComponent(out Health health))
        {
            _audioSource.Play();
            health.TakeDamage(_strengthEffect);
            _poolItems.ReturnObject(this);
        }
    }
}
