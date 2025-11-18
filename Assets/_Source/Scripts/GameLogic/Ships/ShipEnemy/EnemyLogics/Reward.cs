using _Source.Scripts.GameLogic.StaticClass.BankResources;
using _Source.Scripts.GamePlay.StaticClass.BankResources;
using UnityEngine;
using YG;

public class Awardee : MonoBehaviour
{
    private Health _health;
    private int _score;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.HealthOver += SetReward;
    }

    private void OnDisable()
    {
        _health.HealthOver -= SetReward;
    }

    public void Init(int score)
    {
        _score = score;
    }

    private void SetReward()
    {
        LocalData.AddScore(_score);
    }
}
