using UnityEngine;

[CreateAssetMenu(fileName = "playerInfo", menuName = "PlayerShip")]
public class PlayerInfo : ScriptableObject
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _speed;
    [SerializeField] private float _shootDelay = 1f;
    [SerializeField] private float _speedBullet = 1f;
    [SerializeField] private float _damage = 15f;

    public int ShipLvl { get; private set; }
    public float MaxHealth => _maxHealth;
    public float Speed => _speed;
    public float ShootDelay => _shootDelay;
    public float SpeedBullet => _speedBullet;
    public float Damage => _damage;

    public void SetMaxHealth(float newValue) => _maxHealth = newValue;
    public void SetSpeed(float newValue) => _speed = newValue;
    public void SetShootDelay(float newValue) => _shootDelay = newValue;
    public void SetSpeedBullet(float newValue) => _speedBullet = newValue;
    public void SetDamage(float newValue) => _damage = newValue;
    public void SetLevel() => ShipLvl++;
}