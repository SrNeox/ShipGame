using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShipsStoreByTime : MonoBehaviour 
{
    [SerializeField] private int _shipNumber;
    [SerializeField] private int _targetTimeUnlock;
    [SerializeField] private BlockImage _blockImage;
    [SerializeField] private TextMeshProUGUI _progress;
    [SerializeField] private Button _button;
    
    public int ShipNumber => _shipNumber;

    public event Action<int> IsSelection;

    private void Start()
    {
        UnLockShips();
        _button.onClick.AddListener(ActiveEvent);
    }

    private void ActiveEvent()
    {
        IsSelection?.Invoke(_shipNumber);
    }

    private void UnLockShips()
    {
        if (YG2.saves._timePlay >= _targetTimeUnlock)
        {
            OnActive();
        }
        else
        {
            OffActive();
            ShowProgress();
        }
    }

    private void OnActive()
    {
        _button.enabled = true;
        _blockImage.gameObject.SetActive(false);
        _progress.gameObject.SetActive(false);
    }

    private void OffActive()
    {
        _button.enabled = false;
        _blockImage.gameObject.SetActive(true);
        _progress.gameObject.SetActive(true);
    }

    private void ShowProgress()
    {
        float offSetTime = _targetTimeUnlock - YG2.saves._timePlay;
        var time = TimeSpan.FromSeconds(offSetTime);

        _progress.text = $"{time.Minutes:D2}:{time.Seconds:D2}";
    }
}

