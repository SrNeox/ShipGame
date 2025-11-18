using UnityEngine;
using DG.Tweening;

using UnityEngine;

public class MaterialOffsetAddY : MonoBehaviour
{
    [SerializeField] private Renderer _targetRenderer; 
    [SerializeField] private float _speedY = 0.2f;     

    private Material _mat;

    private void Awake()
    {
        if (_targetRenderer != null)
            _mat = _targetRenderer.material; 
    }

    private void Update()
    {
        if (_mat == null) return;

        Vector2 offset = _mat.mainTextureOffset;
        offset.y += _speedY * Time.deltaTime;
        
        if (offset.y >= 1f || offset.y <= -1f)
            offset.y = Mathf.Repeat(offset.y, 1f);

        _mat.mainTextureOffset = offset;
    }
}
