using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class OffSound : MonoBehaviour
{
    private const string MasterVolume = "MasterVolume";

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TextMeshProUGUI _textOnOff;
    [SerializeField] private Slider _volumeMasterVolume;

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.isOn = false;
        _toggle.onValueChanged.AddListener(OffAllSound);
    }

    private void OffAllSound(bool isOn)
    {
        if (isOn)
        {
            _audioMixer.SetFloat(MasterVolume, -80);
        }
        else
        {
            float value = Mathf.Max(0.0001f, _volumeMasterVolume.value);
            _audioMixer.SetFloat(MasterVolume, Mathf.Log10(value) * 20f);
        }
    }
}
