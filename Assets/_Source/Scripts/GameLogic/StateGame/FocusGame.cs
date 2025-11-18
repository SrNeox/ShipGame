using UnityEngine;
using YG;
using static UIStateMachine;

namespace _Source.Scripts
{
    public class FocusGame : MonoBehaviour
    {
        private UIStateMachine _uiStateMachine;

        private void Start()
        {
            _uiStateMachine = FindAnyObjectByType<UIStateMachine>();
        }

        private void OnEnable()
        {
            YG2.onFocusWindowGame += Focuses;
        }

        private void OnDisable()
        {
            YG2.onFocusWindowGame -= Focuses;
        }

        private void Focuses(bool active)
        {
            if (_uiStateMachine != null)
            {
                FocusGamePlayScene(active);
            }
            else
            {
                FocusesMenuScene(active);
            }
        }

        private void FocusGamePlayScene(bool active)
        {
            if (active == true)
            {
                StateGame.ResumeSoud();

                if (_uiStateMachine.CurrentUI == UIState.ActiveGameUi)
                {
                    StateGame.ResumeGame();
                }
            }

            if (active == false)
            {
                StateGame.PauseGame();
                StateGame.PausedSoud();
            }
        }

        private void FocusesMenuScene(bool active)
        {
            if (active == true)
            {
                StateGame.ResumeSoud();
                StateGame.ResumeGame();
            }

            if (active == false)
            {
                StateGame.PauseGame();
                StateGame.PausedSoud();
            }
        }
    }
}