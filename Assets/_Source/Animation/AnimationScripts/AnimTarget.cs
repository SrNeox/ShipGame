using DG.Tweening;
using UnityEngine;

public class AnimTarget : MonoBehaviour
{
    private Tweener _animation;

    private void OnEnable()
    {
        _animation = transform.DOScale(0.5f, 5f);

        Destroy(gameObject, 5f);
    }

    private void OnDestroy()
    {
        _animation?.Kill();
    }
}
