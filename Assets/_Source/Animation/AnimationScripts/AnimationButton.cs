using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Source.Animation.AnimationScripts
{
    [RequireComponent(typeof(RectTransform))]
    public class AnimationButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _hoverScale = 1.1f;
        [SerializeField] private float _duration = 0.15f;
        [SerializeField] private bool _originalScaleOnStart = true;

        private Vector3 _originalScale;
        private RectTransform _rectTransform;
        private Tween _currentTween;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (_originalScaleOnStart)
                _originalScale = _rectTransform.localScale;
        }
    
        private void Start()
        {
            if (_originalScaleOnStart)
                _originalScale = _rectTransform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _currentTween?.Kill();

            Vector3 target = new Vector3(_originalScale.x * _hoverScale,
                _originalScale.y * _hoverScale,
                _originalScale.z * _hoverScale);

            _currentTween = _rectTransform.DOScale(target, _duration).SetEase(Ease.Linear);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _currentTween?.Kill();
            _currentTween = _rectTransform.DOScale(_originalScale, _duration).SetEase(Ease.Linear);
        }

        void OnDisable()
        {
            _currentTween?.Kill();

            if (_rectTransform != null)
                _rectTransform.localScale = _originalScale;
        }
    }
}