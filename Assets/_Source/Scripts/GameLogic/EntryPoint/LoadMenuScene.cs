using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Source.Scripts
{
    public class LoadMenuScene : MonoBehaviour
    {
        private readonly int _numberSceneMenu = 1;

        private void Start()
        {
            SceneManager.LoadScene(_numberSceneMenu);
        }
    }
}
