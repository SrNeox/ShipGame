using UnityEngine;

public class StateGame : MonoBehaviour
{
    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public static void ResumeSoud()
    {
        AudioListener.pause = false;
    }

    public static void PausedSoud()
    {
        AudioListener.pause = true;
    }
}
