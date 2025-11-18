using UnityEngine;

public class SpawnerPlayer : MonoBehaviour
{
    [SerializeField] private Player[] _players;
    [SerializeField] private GameOver _stopGame;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        Player player = Instantiate(_players[SelectedShip.NumberShip], transform.position, Quaternion.identity);
        _stopGame.SetHealthPlayer(player.GetComponent<Health>());
    }
}