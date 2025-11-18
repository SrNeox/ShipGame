using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private Transform[] _pointPosition;

    private float _speed;
    private float _speedScale = 1f;
    private int _targetPoint;

    private void Update()
    {
        Move(_speed);
    }

    public void Init(float valueSpeed, Transform[] movePosition)
    {
        _speed = valueSpeed;
        _pointPosition = movePosition;
    }

    public void SetSpeedScale(float value = 1)
    {
        _speedScale = value;
    }

    public void Move(float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, _pointPosition[_targetPoint].position, speed * Time.deltaTime * _speedScale);

        if ((transform.position - _pointPosition[_targetPoint].position).sqrMagnitude < 0.01f)
        {
            TakeRandomPoint();
        }
    }

    private void TakeRandomPoint()
    {
        _targetPoint = RandomNumber.Create(0, _pointPosition.Length);
    }
}
