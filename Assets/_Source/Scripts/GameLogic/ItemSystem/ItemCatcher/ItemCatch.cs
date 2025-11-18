using Reflex.Attributes;
using UnityEngine;

public class ItemCatch : MonoBehaviour
{
    [Inject] private PoolItems _poolBomb;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.TryGetComponent(out Bomb bomb))
        {
            _poolBomb.ReturnObject(bomb);
            Debug.Log("catch");
        }
    }
}
