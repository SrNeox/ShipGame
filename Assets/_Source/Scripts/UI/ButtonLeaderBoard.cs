using UnityEngine;
using YG;

public class ButtonLeaderBoard : MonoBehaviour
{
    [SerializeField] private Canvas _leaderboardCanvas;
    [SerializeField] private Canvas _canvasAuth;
    [SerializeField] private Canvas _canvasClose;

    public void ShowAuthWindow()
    {
        if (YG2.player.auth == false)
        {
            _canvasAuth.gameObject.SetActive(true);
        }
        else
        {
            _leaderboardCanvas.gameObject.SetActive(true);
        }

        _canvasClose.gameObject.SetActive(false);
    }
}
