using Reflex.Core;
using UnityEngine;

public class SceneInstailer : MonoBehaviour, IInstaller
{
    private const string MovePointName = "MovePoint";

    [SerializeField] private PoolItems _poolItems;
    [SerializeField] private PoolBullet _poolBullet;
    [SerializeField] private PoolEnemies _poolEnemies;

    [Header("Enemy Ship")]
    [SerializeField] private Transform[] _movePoint;

    public void InstallBindings(ContainerBuilder container)
    {
        container.AddSingleton(_poolItems);   
        container.AddSingleton(_poolBullet);  
        container.AddSingleton(_poolEnemies);  
        container.AddSingleton(this);
        container.AddSingleton(_movePoint).SetName(MovePointName);
    }
}
