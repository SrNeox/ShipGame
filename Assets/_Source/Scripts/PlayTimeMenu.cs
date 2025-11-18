using System;
using TMPro;
using UnityEngine;
using YG;

public class PlayTimeMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    private void OnEnable()
    {
        var time = TimeSpan.FromSeconds(YG2.saves._timePlay);

        _timerText.text = $"{time.Minutes:D2}:{time.Seconds:D2}";
    }
}