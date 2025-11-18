using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] protected float _strengthEffect;
    [SerializeField] protected AudioSource _audioSource;

    protected void Move()
    {
        transform.Translate(0, 0, -_speed * Time.deltaTime);
    }
}
