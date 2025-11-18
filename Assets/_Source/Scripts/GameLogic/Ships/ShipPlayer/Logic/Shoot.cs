using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DrawLine))]
[RequireComponent(typeof(InputPlayer))]
public class Shoot : MonoBehaviour
{
    private PoolBullet _pool;
    private Transform _firePoint;
    private DrawLine _drawLine;
    private AudioSource _audioSource;

    private bool _canShoot = true;
    private float _damage;
    private float _speed;

    private Coroutine _shootDelay;
    private Coroutine _moveLineBullet;
    private WaitForSeconds _delay;

    private void Awake()
    {
        _drawLine = GetComponent<DrawLine>();
        _drawLine.OnLineDrawn += ShooBulletUI; 

        _pool = FindObjectOfType<PoolBullet>();

        if (_pool == null)
        {
            Debug.LogError("PoolBullet not found in scene!");
        }
    }

    private void OnDestroy()
    {
        _drawLine.OnLineDrawn -= ShooBulletUI; 
    }

    public void Init(Transform firePoint, float delay, float damage, float speed, AudioSource audioSource)
    {
        _delay = new WaitForSeconds(delay);
        _damage = damage;
        _speed = speed;
        _firePoint = firePoint;
        _audioSource = audioSource;
    }

    public void ShooBulletUI()
    {
        if (_canShoot && _drawLine.MousePositionList.Count >= 2) 
        {
            if (_shootDelay != null)
                StopCoroutine(_shootDelay);

            _shootDelay = StartCoroutine(ShootDelay());
        }
    }

    private IEnumerator ShootDelay()
    {
        _canShoot = false;

        if (_moveLineBullet != null)
            StopCoroutine(_moveLineBullet);

        _moveLineBullet = StartCoroutine(MoveLineBullet());

        yield return _delay;

        _canShoot = true;
    }

    private Bullet CreateBullet()
    {
        if (_pool == null)
        {
            _pool = FindObjectOfType<PoolBullet>();

            if (_pool == null)
            {
                return null;
            }
        }

        Bullet bullet = _pool.GetObject();
        bullet.Init(_firePoint, _damage, _speed, gameObject); 

        if (_audioSource != null)
        {
            _audioSource.Play();
        }

        return bullet;
    }

    private IEnumerator MoveLineBullet()
    {
        if (_drawLine.MousePositionList.Count < 2)
            yield break;

        var bullet = CreateBullet();

        if (bullet == null)
        {
            _canShoot = true;
            yield break;
        }


        Vector3 startPoint = _drawLine.MousePositionList[0];
        Vector3 endPoint = _drawLine.MousePositionList[_drawLine.MousePositionList.Count - 1];


        float distance = Vector3.Distance(startPoint, endPoint);
        float journey = 0f;

        while (bullet != null && journey < distance)
        {
            journey += Time.deltaTime * _speed;
            float fractionOfJourney = journey / distance;

            bullet.transform.position = Vector3.Lerp(startPoint, endPoint, fractionOfJourney);


            Vector3 direction = (endPoint - startPoint).normalized;
            bullet.transform.forward = direction;

            yield return null;
        }
    }
}