using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Canvas _openedCanvas;
    [SerializeField] private Canvas _closedCanvas;

    private UnityEngine.UI.Button _button;
    private GameObject _canvasOpened;
    private GameObject _canvasClosed;

    private void Awake()
    {
        _button = GetComponent<UnityEngine.UI.Button>();
        _button.onClick.AddListener(HandleClick);

        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }

        if (_openedCanvas != null)
        {
            _canvasOpened = _openedCanvas.gameObject;
        }

        if (_closedCanvas != null)
        {
            _canvasClosed = _closedCanvas.gameObject;
        }
    }

    private void HandleClick()
    {
        PlaySound();

        if (_openedCanvas != null)
        {
            _canvasOpened.SetActive(!_canvasOpened.activeSelf);
        }

        if (_closedCanvas != null)
        {
            _canvasClosed.SetActive(!_canvasClosed.activeSelf);
        }
    }

    private void PlaySound()
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
    }
}
