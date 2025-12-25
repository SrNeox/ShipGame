using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(DrawLine))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Shoot))]
[RequireComponent(typeof(Player))]
public class PlayerShip : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfo;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LayerMask _groundMask;

    private DrawLine _drawLine;
    private Mover _mover;
    private Shoot _shoot;
    private Health _health;
    private AudioSource _audioSource;

    private float _valueHealth = 1;
    private float _maxHealth;
    private float _speed;
    private float _shootDelay;
    private float _speedBullet;
    private float _damage;

    private void Awake()
    {
        SetComponent();
        SetInfoShip();
        Init();
    }

    private void Init()
    {
        _drawLine.Init(_groundMask);
        _shoot.Init(_firePoint, _shootDelay, _damage, _speedBullet, _audioSource);
        _mover.Init(_speed);
        _health.Init(_valueHealth, _maxHealth);
    }

    private void SetInfoShip()
    {
        _maxHealth = _playerInfo.MaxHealth;
        _speed = _playerInfo.Speed;
        _shootDelay = _playerInfo.ShootDelay;
        _speedBullet = _playerInfo.SpeedBullet;
        _damage = _playerInfo.Damage;

        _valueHealth = _maxHealth;
    }

    private void SetComponent()
    {
        _mover = GetComponent<Mover>();
        _shoot = GetComponent<Shoot>();
        _drawLine = GetComponent<DrawLine>();
        _health = GetComponent<Health>();
        _audioSource = GetComponent<AudioSource>();
    }
}
