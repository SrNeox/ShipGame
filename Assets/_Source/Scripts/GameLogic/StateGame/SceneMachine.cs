using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMachine : MonoBehaviour
{
    private int _sceneMenu = 1;
    private int _sceneGame = 2;

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_sceneMenu);
    }

    public void LoadGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(_sceneGame);
    }
}
