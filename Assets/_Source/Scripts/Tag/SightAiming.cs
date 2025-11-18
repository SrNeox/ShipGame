using _Source.Scripts.GameLogic.StateGame;
using UnityEngine;
using DG.Tweening;
using _Source.Scripts.GameLogic.StateGame;
using UnityEngine;
using DG.Tweening;

namespace _Source.Scripts.Tag
{
    public class SightAiming : MonoBehaviour
    {
        private const int ZoomInEnemy = 3;

        [Header("Movement")] [SerializeField] private float _speedTransformAim = 5f;

        [Header("DOTween Pulse / Rotation")]
        [Tooltip("Относительный множитель масштаба при пульсации (1 = оригинал)")]
        [SerializeField]
        private float _pulseMultiplier = 1.08f;

        [SerializeField] private float _pulseDuration = 0.6f;

        [Tooltip("Градусов в секунду для вращения по Y (маленькое значение = медленно)")] [SerializeField]
        private float _rotationSpeedDegPerSecond = 60f;

        [SerializeField] private Ease _tweenEase = Ease.InOutSine;

        private StrikerSwitch _strikerSwitchker;
        private Camera _camera;
        private Transform _target;
        private Vector3 _offsetPositionY;
        private Vector3 _defaultPositionCamera;

        private bool _isEnemy;

        private Vector3 _originalScale;
        private Tween _pulseTween;
        private Tween _rotationTween;

        private void Awake()
        {
            _strikerSwitchker = FindObjectOfType<StrikerSwitch>();
            _camera = FindObjectOfType<Camera>();
            if (_camera != null)
                _defaultPositionCamera = _camera.transform.position;

            _originalScale = transform.localScale;
        }

        private void OnEnable()
        {
            if (_strikerSwitchker != null)
            {
                _strikerSwitchker.OnSwitchTarget += OnSetTarget;
                _strikerSwitchker.OnEnemyDie += OnSetTarget;
            }

            StartPulseAndRotation();
        }

        private void OnDisable()
        {
            if (_strikerSwitchker != null)
            {
                _strikerSwitchker.OnSwitchTarget -= OnSetTarget;
                _strikerSwitchker.OnEnemyDie -= OnSetTarget;
            }

            StopTweensAndReset();
        }

        private void Update()
        {
            MoveAimTarget();
        }

        private void MoveAimTarget()
        {
            if (_target == null) return;

            MovingSight();
            MovingCamera();
        }

        private void MovingCamera()
        {
            if (_camera == null) return;

            _camera.transform.position = Vector3.Lerp(
                _camera.transform.position,
                new Vector3(_target.position.x, _camera.transform.position.y, _camera.transform.position.z),
                _speedTransformAim * Time.deltaTime);

            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _offsetPositionY,
                _speedTransformAim * Time.deltaTime);
        }

        private void MovingSight()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(_target.position.x, transform.position.y, _target.position.z),
                _speedTransformAim * Time.deltaTime);
        }

        private void OnSetTarget(Transform target, bool isEnemy)
        {
            _target = target;
            _isEnemy = isEnemy;
            AdjustPosition();
        }

        private void AdjustPosition()
        {
            if (_camera == null) return;

            if (_isEnemy)
            {
                _offsetPositionY = new Vector3(
                    _camera.transform.position.x,
                    _camera.transform.position.y - ZoomInEnemy,
                    _camera.transform.position.z);
            }
            else
            {
                _offsetPositionY = _defaultPositionCamera;
            }

            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _offsetPositionY,
                _speedTransformAim * Time.deltaTime);
        }

        private void StartPulseAndRotation()
        {
            // Пульсация: от originalScale до originalScale * pulseMultiplier и обратно
            if (_pulseTween == null || !_pulseTween.IsActive())
            {
                Vector3 targetScale = _originalScale * _pulseMultiplier;
                _pulseTween = transform.DOScale(targetScale, _pulseDuration)
                    .SetEase(_tweenEase)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetId(this);
            }

            // Вращение по Y на 360 градусов бесконечно
            if (_rotationTween == null || !_rotationTween.IsActive())
            {
                float stepAngle = 360f; // полный оборот за один цикл
                float durationForStep = Mathf.Abs(stepAngle) / Mathf.Max(0.0001f, _rotationSpeedDegPerSecond);

                _rotationTween = transform
                    .DORotate(new Vector3(0f, stepAngle, 0f), durationForStep, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Incremental)
                    .SetId(this);
            }
        }

        private void StopTweensAndReset()
        {
            if (_pulseTween != null && _pulseTween.IsActive())
            {
                _pulseTween.Kill();
                _pulseTween = null;
            }

            if (_rotationTween != null && _rotationTween.IsActive())
            {
                _rotationTween.Kill();
                _rotationTween = null;
            }

            // Сброс масштаба и остановка любых оставшихся твинов на трансформе
            transform.localScale = _originalScale;
            transform.DOKill(false);
        }
    }
}