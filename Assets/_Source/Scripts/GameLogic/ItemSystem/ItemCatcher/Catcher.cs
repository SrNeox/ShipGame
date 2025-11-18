using Reflex.Attributes;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    [Inject] private readonly PoolBullet _poolBullet;
    [Inject] private readonly PoolItems _poolItems;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Bullet bullet))
        {
            _poolBullet.ReturnObject(bullet);
        }

        if (collider.TryGetComponent(out Item item))
        {
            _poolItems.ReturnObject(item);
        }
    }
}