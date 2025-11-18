using UnityEngine;
using YG;

public class SetValue : MonoBehaviour
{
    [SerializeField] private float _timeToSet;
    [SerializeField] private int _scoreSet;

    bool _isSet = false;
    
    private void Awake()
    {
        Set();
    }

    private void Set()
    {
        if (_isSet != false) return;
        
        YG2.saves._timePlay =  _timeToSet;
        YG2.saves.Score =  _scoreSet;

    }
}
