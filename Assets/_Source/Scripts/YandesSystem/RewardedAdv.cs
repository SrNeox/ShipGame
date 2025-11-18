using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class RewardedAdv : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private UIStateMachine _stateMachineUi;
    [SerializeField] private PoolBullet _bulletPool;

    private Button _rewardedButton;

    public string rewardID;

    private void Start()
    {
        _rewardedButton = GetComponent<Button>();

        _player = FindAnyObjectByType<Player>();

        _playerHealth = _player.GetComponent<Health>();

        _rewardedButton.onClick.AddListener(RevivePlayer);
    }

    private void RevivePlayer()
    {
        YG2.RewardedAdvShow(rewardID, () =>
        {
            StateGame.ResumeGame();

            _bulletPool.ReaturnAllObject();
            _playerHealth.SetHealth();
            _stateMachineUi.ShowContinue();
            _rewardedButton.gameObject.SetActive(false);
        });
    }
}