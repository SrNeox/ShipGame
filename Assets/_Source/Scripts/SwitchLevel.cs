using System;
using System.Collections;
using UnityEngine;

public class SwitchLevel : MonoBehaviour
{
    [SerializeField] private SpawnerEnemy[] _spawnersEnemy;
    [SerializeField] private SpawnerItems[] _spawnersItem;
    [SerializeField] private Transform[] _pointsMove;
    [SerializeField] private float _moveSpeed = 5f;

    private InputPlayer _inputPlayer;
    private Coroutine _movePlayerCoroutine;
    private Health _enemyHealth;
    private Player _player;

    private int _currentLevel;
    private int _currentScore;
    private int _targetScore;

    private bool _isEvenLevel = false;

    public Player GetPlayer => _player ??= FindAnyObjectByType<Player>();

    private void Start()
    {
        _targetScore = 4;
        _currentScore = 0;
        _currentLevel = 0;

        _player = GetPlayer;
        if (_player == null)
        {
            Debug.LogError("Player not found in scene.");
            enabled = false;
            return;
        }

        _inputPlayer = _player.GetComponent<InputPlayer>();
        if (_inputPlayer == null)
        {
            Debug.LogError("InputPlayer component not found on Player.");
        }

        // Отключаем все спавнеры кроме первого (если нужно)
        Initialized();

        // Подписываемся на текущего врага (если есть)
        TrySubscribeToCurrentEnemy();
    }

    public void Initialized()
    {
        DisableSpawners(_spawnersItem);
        DisableSpawners(_spawnersEnemy);
    }

    private void NextLevel()
    {
        // Увеличиваем счёт, если ещё не достигли цели
        if (_currentScore < _targetScore)
        {
            _currentScore++;
            return;
        }

        // Подготовка к следующему уровню
        _targetScore += _isEvenLevel ? 4 : 3;

        // Отключаем текущие спавнеры (проверки границ)
        if (IsValidIndex(_spawnersEnemy, _currentLevel))
            _spawnersEnemy[_currentLevel].enabled = false;
        if (IsValidIndex(_spawnersItem, _currentLevel))
            _spawnersItem[_currentLevel].enabled = false;

        _isEvenLevel = !_isEvenLevel;
        _currentLevel++;

        // Проверка на конец уровней
        if (!IsValidIndex(_spawnersEnemy, _currentLevel) || !IsValidIndex(_pointsMove, _currentLevel))
        {
            Debug.Log("No more levels or points defined.");
            return;
        }

        // Останавливаем предыдущую корутину и запускаем новую
        if (_movePlayerCoroutine != null)
            StopCoroutine(_movePlayerCoroutine);

        _movePlayerCoroutine = StartCoroutine(MovePlayer(_pointsMove[_currentLevel].position));
    }

    private IEnumerator MovePlayer(Vector3 targetPosition)
    {
        if (_inputPlayer != null)
            _inputPlayer.enabled = false;

        // Плавное движение к точке
        while ((_player.transform.position - targetPosition).sqrMagnitude > 0.001f)
        {
            _player.transform.position = Vector3.MoveTowards(
                _player.transform.position,
                targetPosition,
                _moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        // Убедимся, что позиция точная
        _player.transform.position = targetPosition;

        if (_inputPlayer != null)
            _inputPlayer.enabled = true;

        // Включаем спавнеры текущего уровня
        if (IsValidIndex(_spawnersEnemy, _currentLevel))
        {
            _spawnersEnemy[_currentLevel].enabled = true;
            _spawnersEnemy[_currentLevel].Initialized();
        }

        if (IsValidIndex(_spawnersItem, _currentLevel))
            _spawnersItem[_currentLevel].enabled = true;

        // Подписываемся на нового врага (отписываемся от старого внутри)
        TrySubscribeToCurrentEnemy();
    }

    private void TrySubscribeToCurrentEnemy()
    {
        // Отписываемся от предыдущего
        if (_enemyHealth != null)
        {
            _enemyHealth.HealthOver -= NextLevel;
            _enemyHealth = null;
        }

        if (!IsValidIndex(_spawnersEnemy, _currentLevel))
            return;

        var enemy = _spawnersEnemy[_currentLevel].Enemy;
        if (enemy == null)
        {
            Debug.LogWarning($"SpawnerEnemy at {_currentLevel} has no Enemy assigned.");
            return;
        }

        var health = enemy.GetComponent<Health>();
        if (health == null)
        {
            Debug.LogWarning("Enemy has no Health component.");
            return;
        }

        _enemyHealth = health;
        _enemyHealth.HealthOver += NextLevel;
    }

    private bool IsValidIndex(Array arr, int index)
    {
        return arr != null && index >= 0 && index < arr.Length;
    }

    private void DisableSpawners(Behaviour[] spawners)
    {
        if (spawners == null || spawners.Length == 0) return;

        // Если нужно отключить все — начать с 0, иначе с 1 как у вас было
        for (int i = 0; i < spawners.Length; i++)
        {
            if (spawners[i] != null)
                spawners[i].enabled = false;
        }
    }

    private void OnDestroy()
    {
        if (_enemyHealth != null)
            _enemyHealth.HealthOver -= NextLevel;
    }
}
