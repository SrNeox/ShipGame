using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ChangerVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Toggle _toggle;

    private Slider _volumeSlider;

    private float _minValue = 0.0001f;
    private float _decibelConversion = 20f;

    public float ConvertVoluve { get; private set; }

    private void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        if (_toggle.isOn == false)
        {
            float value = Mathf.Max(_minValue, volume);
            string volumeGroup = _volumeSlider.name;

            _audioMixer.SetFloat(volumeGroup, Mathf.Log10(value) * _decibelConversion);
        }
    }
}
