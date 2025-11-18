using System;
using _Source.Scripts.GameLogic.Ships.ShipEnemy;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ShipIconProgress : MonoBehaviour
{
    private const string NameAnimTrigger = "Play";
    private const string NameAnimTrigger2 = "Return";

    [SerializeField] private Animator[] _shipIconAnimator;
    [SerializeField] private ShipIcon[] _shipIcon2;
    [SerializeField] private Image _icon;

    private Health _health;
    private Vector3 _startPosition;
    private int _currnetShipIcon = 0;

    private void Start()
    {
        _startPosition = _icon.transform.position;
        MoveImageTarget();
    }

    private void OnDestroy()
    {
        if (_health != null)
            _health.HealthOver -= PlayAnimation;
    }

    public void GetEnemy([NotNull] EnemyShip enemy)
    {
        if (enemy == null)
        {
            throw new ArgumentNullException(nameof(enemy));
        }
        
        Health health = enemy.GetComponent<Health>();

        health.HealthOver += PlayAnimation;

        _health = health;
    }

    private void PlayAnimation()
    {
        Reset();

        if (_currnetShipIcon < _shipIconAnimator.Length)
        {
            _shipIconAnimator[_currnetShipIcon].SetTrigger(NameAnimTrigger);
            _currnetShipIcon++;
            _health.HealthOver -= PlayAnimation;
            MoveImageTarget();
        }

        Invoke(nameof(Reset), 1);
    }

    private void Reset()
    {
        if (_currnetShipIcon == _shipIconAnimator.Length)
        {
            _currnetShipIcon = 0;

            foreach (var ship in _shipIconAnimator)
            {
                ship.SetTrigger(NameAnimTrigger2);
            }
        }
    }

    private void MoveImageTarget()
    {
        if (_currnetShipIcon < _shipIconAnimator.Length)
            _icon.transform.position = _shipIcon2[_currnetShipIcon].transform.position;
        else
            _icon.transform.position = _startPosition;
    }
}