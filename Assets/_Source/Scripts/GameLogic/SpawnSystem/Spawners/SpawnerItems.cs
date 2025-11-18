using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using System.Collections;
using UnityEngine;

public class SpawnerItems : MonoBehaviour
{
    [Inject] private readonly PoolItems _poolItems;
    [Inject] private readonly Container _container;

    [SerializeField] private float _delay = 5;
    [SerializeField] private Transform[] _spawnPoint;

    private WaitForSeconds delay;

    private void Start()
    {
        StartCoroutine(SpawnItems());
        delay = new(_delay);
    }

    private IEnumerator SpawnItems()
    {
        while (true)
        {
            TakeItem();

            yield return delay;
        }
    }

    private Item TakeItem()
    {
        var item = _poolItems.GetObject();
        AttributeInjector.Inject(item, _container);

        SetItemPosition(item);

        return item;
    }

    private void SetItemPosition(Item item)
    {
        int randomSpawnPoint = RandomNumber.Create(0, _spawnPoint.Length);
        item.transform.position = _spawnPoint[randomSpawnPoint].position;
    }
}
