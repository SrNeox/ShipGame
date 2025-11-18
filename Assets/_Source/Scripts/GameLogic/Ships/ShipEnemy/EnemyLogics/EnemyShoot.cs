using System.Collections;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

namespace _Source.Scripts.GameLogic.Ships.ShipEnemy.EnemyLogics
{
    [RequireComponent(typeof(SearchPlayer))]
    public class EnemyShoot : MonoBehaviour
    {
        [Inject] private readonly PoolBullet _poolBullet;
        [Inject] private readonly Container _container;

        private Transform[] _firePoints;
        private AudioSource _audioSource;

        private float _fireRate = 1f;
        private float _nextFireTime;
        private float _speedBullet;
        private float _damage;

        private SearchPlayer _searchPlayer;
        private bool _isArcBulletInFlight;

        [SerializeField] private GameObject _landingIndicatorPrefab;
        [SerializeField] private float _arcHeight = 5f;
        [SerializeField] private float _arcDuration = 2f;

        private void Start()
        {
            SetFirePoints();
            _searchPlayer = GetComponent<SearchPlayer>();
        }

        private void Update()
        {
            if (_searchPlayer._playerPosition is not null)
            {
                RotateToPlyer();

                if (Time.time > _nextFireTime)
                {
                    Shoot();
                    _nextFireTime = Time.time + 1f / _fireRate;
                }
            }
        }

        public void Init(float fireRate, Transform[] firePoints, float speedBullet, float damage,
            AudioSource audioSource)
        {
            _speedBullet = speedBullet;
            _damage = damage;
            _firePoints = firePoints;
            _fireRate = fireRate;
            _audioSource = audioSource;
        }

        private void RotateToPlyer()
        {
            Vector3 direction = _searchPlayer._playerPosition.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5);
        }

        private void Shoot()
        {
            for (int i = 0; i < _firePoints.Length; i++)
            {
                bool shootArc = Random.value < 0.3f && !_isArcBulletInFlight;

                if (shootArc)
                    ShootArcBullet(_firePoints[i]);
                else
                    SpawnBullet(_firePoints[i]);
            }
        }

        private void SpawnBullet(Transform firepoint)
        {
            Bullet bullet = _poolBullet.GetObject();
            AttributeInjector.Inject(bullet, _container);
            bullet.Init(firepoint, _damage, _speedBullet, gameObject);
            _audioSource.Play();
        }

        private void ShootArcBullet(Transform firepoint)
        {
            Vector3 targetPosition = _searchPlayer._playerPosition.transform.position;

            GameObject indicator = Instantiate(_landingIndicatorPrefab, targetPosition, Quaternion.identity);

            Bullet bullet = _poolBullet.GetObject();
            AttributeInjector.Inject(bullet, _container);

            _isArcBulletInFlight = true;
            bullet.InitArc(firepoint, _damage, _arcDuration, targetPosition, _arcHeight, indicator, gameObject);
            _audioSource.Play();

            StartCoroutine(ResetArcBulletFlag(_arcDuration));
        }

        private IEnumerator ResetArcBulletFlag(float delay)
        {
            yield return new WaitForSeconds(delay);
            _isArcBulletInFlight = false;
        }

        private void SetFirePoints()
        {
            int countFirePoints = transform.childCount;
            _firePoints = new Transform[countFirePoints];

            for (int i = 0; i < _firePoints.Length; i++)
                _firePoints[i] = transform.GetChild(i);
        }
    }
}