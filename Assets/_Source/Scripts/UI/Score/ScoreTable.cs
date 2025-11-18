using _Source.Scripts.GameLogic.StaticClass.BankResources;
using _Source.Scripts.GamePlay.StaticClass.BankResources;
using TMPro;
using UnityEngine;

public class ScoreTable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;

    private Health _health;

    private void OnEnable()
    {
        UpdateScoreText(); 

        if (_health != null)
            _health.HealthOver += UpdateScoreText;
    }

    private void OnDisable()
    {
        if (_health != null)
            _health.HealthOver -= UpdateScoreText;
    }

    private void UpdateScoreText()
    {
        _textScore.text = $"{LocalData.Score}";
    }

    public void Init(Health health)
    {
        if (_health != null)
        {
            _health.HealthOver -= UpdateScoreText;
        }

        _health = health;
        _health.HealthOver += UpdateScoreText;

        UpdateScoreText();
    }
}