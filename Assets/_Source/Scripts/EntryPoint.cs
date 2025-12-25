using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private SpawnerEnemy[] _spawnerEnemies;
    [SerializeField] private SwitchLevel _switchLevel;

    private void Awake()
    {
        _switchLevel.Initialized();
        _spawnerEnemies[0].Initialized();
    }
}