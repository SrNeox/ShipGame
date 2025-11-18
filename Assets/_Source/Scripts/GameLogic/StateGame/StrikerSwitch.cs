using _Source.Scripts.GameLogic.Ships.ShipEnemy.EnemyLogics;
using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
using YG;

namespace _Source.Scripts.GameLogic.StateGame
{
    public class StrikerSwitch : MonoBehaviour
    {
        [SerializeField] private int _enemyTime = 3;
        [SerializeField] private int _playerTime = 3;
        [SerializeField] private UiHelper _helperCanvas;
        [SerializeField] private Canvas _mobileControllerCanvas;
        [SerializeField] private GameObject _shootField;

        private Health _enemyHealth;
        private Health _playerHealth;
        private EnemyShoot _enemyShoot;
        private DrawLine _drawLine;
        private Player _player;
        private CharacterController _characterController;

        private float _currentTime;
        private float _idleTime;
        private bool _isShootingPhase;
        private bool _hasHintCanvas;
        private bool _hintShown;
        private bool _isEnemy;

        public Player Player => _player ??= FindObjectOfType<Player>();
        public DrawLine DrawLine => _drawLine ??= FindObjectOfType<DrawLine>();

        public event Action<Transform, bool> OnSwitchTarget;
        public event Action<Transform, bool> OnEnemyDie;

        private void Start()
        {
            _hasHintCanvas = _helperCanvas != null;
            FindEnemy();
            FindPlayerHealth();

            if (_enemyHealth != null)
                _enemyHealth.enabled = true;

            EnemyShooting();
            OnSwitchTarget?.Invoke(Player.transform, _isEnemy = false);
        }

        private void Update()
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                if (_isShootingPhase)
                {
                    EnemyShooting();
                }
                else
                {
                    PlayerShoots();
                }
            }

            CheckIdleTime();
        }

        private void OnDestroy()
        {
            if (_enemyHealth != null)
                _enemyHealth.HealthOver -= EnemyDie;
        }

        private void CheckIdleTime()
        {
            if (_isShootingPhase)
            {
                if (DrawLine.IsDrawing)
                {
                    _idleTime = 0f;
                    if (_hintShown && _hasHintCanvas)
                    {
                        _helperCanvas.gameObject.SetActive(false);
                        _hintShown = false;
                    }
                }
                else
                {
                    _idleTime += Time.deltaTime;
                    if (_idleTime > 2f && !_hintShown && _hasHintCanvas)
                    {
                        _helperCanvas.gameObject.SetActive(true);
                        _hintShown = true;
                    }
                }
            }
            else
            {
                _idleTime = 0f;
                if (_hintShown && _hasHintCanvas)
                {
                    DisableHelper();
                }
            }
        }

        private void EnemyShooting()
        {
            _isShootingPhase = false;
            _currentTime = _enemyTime;

            ToggleState(DrawLine, false);
            ToggleState(_enemyShoot, true);
            ToggleState(_playerHealth, true);
            ToggleState(_playerHealth, true);
            

            if (_shootField != null)
                _shootField.gameObject.SetActive(false);

            if (_characterController != null)
                _characterController.enabled = true;

            _idleTime = 0f;

            OnSwitchTarget?.Invoke(Player.transform, _isEnemy = false);

            DisableHelper();

            if (_mobileControllerCanvas != null && YG2.envir.isMobile)
                _mobileControllerCanvas.gameObject.SetActive(true);
        }

        private void DisableHelper()
        {
            if (_hasHintCanvas)
            {
                _helperCanvas.gameObject.SetActive(false);
                _hintShown = false;
            }
        }

        private void PlayerShoots()
        {
            _isShootingPhase = true;
            _currentTime = _playerTime;

            ToggleState(DrawLine, true);
            ToggleState(_enemyShoot, false);
            ToggleState(_playerHealth, false);

            if (_shootField != null)
                _shootField.gameObject.SetActive(true);
            
            if (_characterController != null)
                _characterController.enabled = false;

            OnSwitchTarget?.Invoke(_enemyShoot.transform, _isEnemy = true);

            _idleTime = 0f;

            DisableHelper();

            if (_mobileControllerCanvas != null && YG2.envir.isMobile)
                _mobileControllerCanvas.gameObject.SetActive(false);
        }

        private void FindEnemy()
        {
            _enemyShoot = FindObjectOfType<EnemyShoot>();
            OnSubscribeHealthOver(_enemyShoot);
        }

        private void FindPlayerHealth()
        {
            if (Player != null)
            {
                _playerHealth = Player.GetComponent<Health>();
                _characterController = Player.GetComponent<CharacterController>();
            }
        }

        private void OnSubscribeHealthOver(EnemyShoot ship)
        {
            if (ship == null)
            {
                FindEnemy();
            }

            _enemyHealth = _enemyShoot.GetComponent<Health>();
            _enemyHealth.HealthOver += EnemyDie;
        }

        private void EnemyDie()
        {
            _enemyHealth.HealthOver -= EnemyDie;

            FindEnemy();

            if (_enemyHealth != null)
                _enemyHealth.enabled = true;

            EnemyShooting();

            OnEnemyDie?.Invoke(Player.transform, _isEnemy = false);

            _idleTime = 0f;
        }

        private void ToggleState(Behaviour component, bool isActive)
        {
            if (component != null)
                component.enabled = isActive;
        }
    }
}