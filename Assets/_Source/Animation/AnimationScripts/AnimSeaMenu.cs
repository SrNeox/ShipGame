using DG.Tweening;
using UnityEngine;

public class AnimSeaMenu : MonoBehaviour
{
    private Tweener _rotationTween;

    private void Start()
    {
        _rotationTween = transform.DOMoveX(15, 5).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        _rotationTween?.Kill();
    }
}
