using DG.Tweening;
using UnityEngine;

public class AnimShipMenu : MonoBehaviour
{
    private Tweener _rotationTween;

    private void Start()
    {
        _rotationTween = transform.DORotate(new Vector3(14, 0), 5f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Yoyo).SetRelative();
    }

    private void OnDestroy()
    {
        _rotationTween?.Kill();
    }
}
