using System;
using _Source.Scripts.GameLogic.StaticClass.BankResources;
using UnityEngine;
using YG;

public class PlayTime : MonoBehaviour
{
    private float _time;

    public float CurrentTime => _time;

    private void Update()
    {
        _time += Time.deltaTime;
    }

    private void OnDestroy()
    {
        YG2.saves._timePlay += _time;
    }
}