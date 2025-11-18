using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShipItemView : MonoBehaviour
{
    [SerializeField] private int _shipNumber;
    [SerializeField] private int _targetScoreUnlock;
    [SerializeField] private BlockImage _blockImage;
    [SerializeField] private TextMeshProUGUI _progress;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;

    public event Action<int> IsSelection;

    private void Start()
    {
        UnLockShips();
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(ActiveEvent);
    }

    private void ActiveEvent()
    {
        IsSelection?.Invoke(_shipNumber);
        SelectedShip.Select(_shipNumber);
    }

    private void UnLockShips()
    {
        if (_targetScoreUnlock <= YG2.saves.Score)
        {
            OnActive();
        }
        else
        {
            OffActive();
            UpdateScore();
        }
    }

    private void OnActive()
    {
        _button.enabled = true;
        _blockImage.gameObject.SetActive(false);
        _progress.gameObject.SetActive(false);
        _text.gameObject.SetActive(false);
    }

    private void OffActive()
    {
        _button.enabled = false;
        _blockImage.gameObject.SetActive(true);
        _progress.gameObject.SetActive(true);
        _text.gameObject.SetActive(true);
        _blockImage.enabled = false;
    }

    private void UpdateScore()
    {
        _progress.text = $"{_targetScoreUnlock}|{YG2.saves.Score}";
    }
}