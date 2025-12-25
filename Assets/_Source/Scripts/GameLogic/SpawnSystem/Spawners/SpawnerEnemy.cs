using System;
using _Source.Scripts.GameLogic.Ships.ShipEnemy;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [Inject] private PoolEnemies _poolEnemies;
    [Inject] private Container _container;

    [SerializeField] private int _targetCount;
    [SerializeField] private Transform[] _movePoint;
    [SerializeField] private ScoreTable _scoreTable;
    [SerializeField] private ShipIconProgress _iconProgress;

    private Health _healthEnemy;
    private EnemyShip _enemyShip;

    private int _countInLevel = 0;

    public EnemyShip Enemy => _enemyShip;

    public event Action IsOverEnemy;

    public void Initialized()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (_countInLevel >= _targetCount)
        {
            Debug.Log("Отработал");
            IsOverEnemy?.Invoke();
            return;
        }

        _enemyShip = _poolEnemies.GetObject();
        _enemyShip.transform.SetPositionAndRotation(transform.position, transform.rotation);
        _iconProgress.GetEnemy(_enemyShip);
        
        _countInLevel++;

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
}