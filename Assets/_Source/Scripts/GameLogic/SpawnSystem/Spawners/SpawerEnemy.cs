using _Source.Scripts.GameLogic.Ships.ShipEnemy;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

public class SpawerEnemy : MonoBehaviour
{
    [Inject] private PoolEnemies _poolEnemies;
    [Inject] private Container _container;

    [SerializeField] private Transform[] _movePoint;
    [SerializeField] private ScoreTable _scoreTable;
    [SerializeField] private ShipIconProgress _iconProgress;

    private Health _healthEnemy;
    private EnemyShip _enemyShip;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        _enemyShip = _poolEnemies.GetObject();
        _enemyShip.transform.SetPositionAndRotation(transform.position, transform.rotation);
        _iconProgress.GetEnemy(_enemyShip);

        InitEnemy();
    }

    private void ReturnShip()
    {
        _healthEnemy.HealthOver -= ReturnShip;
        _enemyShip.RestoreHealth();
        _enemyShip.Buff();
        _poolEnemies.ReturnObject(_enemyShip);

        Spawn();
    }

    private void InitEnemy()
    {
        AttributeInjector.Inject(_enemyShip, _container);

        _healthEnemy = _enemyShip.GetComponent<Health>();

        _healthEnemy.HealthOver += ReturnShip;
        _scoreTable.Init(_healthEnemy);

        if (_enemyShip.MovePoints == null)
        {
            _enemyShip.SetMovePoints(_movePoint);
        }

        _enemyShip.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }

    private void DestroyBoss()
    {
        _healthEnemy.HealthOver -= DestroyBoss;
        _enemyShip.Buff();
        Destroy(_enemyShip.gameObject);
    }
}