using UnityEngine;
using YG;

public class UIStateMachine : MonoBehaviour
{
    public enum UIState
    {
        Tutorial,
        Settings,
        MainMenu,
        GameOver,
        ActiveGameUi,
        StartMenu,
        LeaderBoard,
        MobileControl,
        Continue
    }

    [Header("Canvas References")] [SerializeField]
    private Canvas _startMenuCanvas;

    [SerializeField] private Canvas _mainMenuCanvas;
    [SerializeField] private Canvas _settingsCanvas;
    [SerializeField] private Canvas _tutorialCanvas;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private Canvas _activeGameUiCanvas;
    [SerializeField] private Canvas _leaderBoard;
    [SerializeField] private Canvas _mobileControl;
    [SerializeField] private Canvas _continue;
    [SerializeField] private AudioSource _audioSource;

    private UIState _currentState = UIState.Tutorial;

    public UIState CurrentUI => _currentState;

    private void Awake()
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SwitchState(UIState.StartMenu);
    }

    public void ShowTutorial() => SwitchState(UIState.Tutorial);

    public void ShowSettings() => SwitchState(UIState.Settings);

    public void ShowMainMenu() => SwitchState(UIState.MainMenu);

    public void ShowGameOver() => SwitchState(UIState.GameOver);

    public void ShowStartMenu() => SwitchState(UIState.StartMenu);

    public void ShowContinue() => SwitchState(UIState.Continue);

    public void ShowActiveGameUi()
    {
        if (YG2.saves.IsTutorialShow == false)
        {
            ShowTutorial();
            YG2.saves.IsTutorialShow = true;
        }
        else
        {
            SwitchState(UIState.ActiveGameUi);
        }
    }

    public void ShowLeaderBoard()
    {
        if (YG2.player.auth)
        {
            SwitchState(UIState.LeaderBoard);
        }
        else
        {
            YG2.OpenAuthDialog();
        }
    }

    private void SwitchState(UIState newState)
    {
        if (_currentState == newState)
            return;

        if (_audioSource != null)
            _audioSource.Play();

        SetCanvasActive(_currentState, false);

        ChangeGameState(newState);
        CheckDevice(newState);

        _currentState = newState;
        SetCanvasActive(newState, true);
    }

    private void SetCanvasActive(UIState state, bool isActive)
    {
        switch (state)
        {
            case UIState.MainMenu:
                _mainMenuCanvas.gameObject.SetActive(isActive);
                break;
            case UIState.Continue:
                _continue.gameObject.SetActive(isActive);
                break;
            case UIState.Settings:
                _settingsCanvas.gameObject.SetActive(isActive);
                break;
            case UIState.Tutorial:
                _tutorialCanvas.gameObject.SetActive(isActive);
                break;
            case UIState.GameOver:
                _gameOverCanvas.gameObject.SetActive(isActive);
                break;
            case UIState.ActiveGameUi:
                _activeGameUiCanvas.gameObject.SetActive(isActive);
                break;
            case UIState.StartMenu:
                _startMenuCanvas.gameObject.SetActive(isActive);
                break;
            case UIState.LeaderBoard:
                _leaderBoard.gameObject.SetActive(isActive);
                break;
        }
    }

    private static void ChangeGameState(UIState newState)
    {
        if (newState == UIState.ActiveGameUi)
            StateGame.ResumeGame();
        else
            StateGame.PauseGame();
    }

    private void CheckDevice(UIState newState)
    {
        if (newState == UIState.ActiveGameUi && YG2.envir.isMobile)
        {
            EnableMobileControl();
        }
        else
        {
            DisableMobileControl();
        }
    }

    private void DisableMobileControl()
    {
        _mobileControl.gameObject.SetActive(false);
    }

    private void EnableMobileControl()
    {
        _mobileControl.gameObject.SetActive(true);
    }
}