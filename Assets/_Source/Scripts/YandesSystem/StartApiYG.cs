using UnityEngine;
using YG;

public class StartApiYG : MonoBehaviour
{
    private void Awake()
    {
        YG2.GameReadyAPI();
    }
}
