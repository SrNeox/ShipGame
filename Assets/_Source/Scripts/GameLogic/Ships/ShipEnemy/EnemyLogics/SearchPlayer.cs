using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SearchPlayer : MonoBehaviour
{
    private LayerMask _player;
    private Vector3 _boxSize = new(30,5,30);

    public Transform _playerPosition { get; private set; }

    private void Update()
    {
        _playerPosition = Detection();
    }

    public void Init(LayerMask layerMask)
    {
        _player = layerMask;
    }

    private Transform Detection()
    {
        Collider[] playerPosition = Physics.OverlapBox(transform.position, _boxSize, Quaternion.identity, _player);

        foreach (Collider collider in playerPosition)
        {
            if (collider.TryGetComponent(out Mover player))
            {
                return player.transform;
            }
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        Color _colorBox = new Color(1, 0, 0, 0.5f);
        Gizmos.color = _colorBox;
        Gizmos.DrawCube(transform.position, _boxSize);
    }
}
