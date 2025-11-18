using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InputPlayer))]
public class DrawLine : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private LayerMask _planeLayerMask;
    private InputPlayer _inputPlayer;

    private bool _isDrawing = false;
    private bool _isOverDrawZone = false;

    public List<Vector3> MousePositionList { get; private set; }
    public event Action OnLineDrawn;
    public bool IsDrawing => _isDrawing;

    private void Awake()
    {
        _inputPlayer = GetComponent<InputPlayer>();
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.widthCurve = AnimationCurve.Constant(0, 1, 0.3f);
        _lineRenderer.positionCount = 0;

        MousePositionList = new List<Vector3>();
    }

    private void Start()
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }
    }

    private void Update()
    {
        CheckDrawLine();
        CheckDrawZone();
    }

    private void OnDisable()
    {
        Clear();
    }

    public void Init(LayerMask layerMask)
    {
        _planeLayerMask = layerMask;
    }

    private void CheckDrawZone()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _isOverDrawZone = Physics.Raycast(ray, Mathf.Infinity, _planeLayerMask);
    }

    private void DrawVector()
    {
        if (!_isOverDrawZone) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _planeLayerMask))
        {
            Vector3 hitPoint = hit.point;

            if (MousePositionList.Count == 0 ||
                (MousePositionList[MousePositionList.Count - 1] - hitPoint).sqrMagnitude > 0.01f)
            {
                MousePositionList.Add(hitPoint);
                _lineRenderer.positionCount = MousePositionList.Count;
                _lineRenderer.SetPosition(MousePositionList.Count - 1, hitPoint);
            }
        }
    }

    public void Clear()
    {
        MousePositionList.Clear();
        _lineRenderer.positionCount = 0;
    }

    public Vector3? GetLastPoint()
    {
        if (MousePositionList.Count > 0)
            return MousePositionList[MousePositionList.Count - 1];
        return null;
    }

    private void CheckDrawLine()
    {
        if (_inputPlayer.Draw())
        {
            if (!_isDrawing && _isOverDrawZone)
            {
                Clear();
                _isDrawing = true;
            }

            if (_isDrawing)
            {
                DrawVector();
            }
        }
        else if (_isDrawing)
        {
            _isDrawing = false;

            if (MousePositionList.Count > 1)
            {
                Vector3 firstPoint = MousePositionList[0];
                Vector3 lastPoint = MousePositionList[MousePositionList.Count - 1];

                MousePositionList.Clear();
                MousePositionList.Add(firstPoint);
                MousePositionList.Add(lastPoint);

                _lineRenderer.positionCount = 2;
                _lineRenderer.SetPosition(0, firstPoint);
                _lineRenderer.SetPosition(1, lastPoint);

                OnLineDrawn?.Invoke();
            }
            else
            {
                Clear();
            }
        }
    }
}
