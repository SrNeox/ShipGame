using System.Collections.Generic;
using UnityEngine;

namespace _Source.Scripts.GameLogic.SpawnSystem.Pool
{
    public class PoolObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T[] _prefabs;

        private Queue<T> _poolObjects = new();

        private int _currentIndex = 0;

        private void Awake()
        {
            FillPool();
        }

        private void FillPool()
        {
            T item;

            for (int i = 0; i < _prefabs.Length; i++)
            {
                item = Instantiate(_prefabs[i]);
                item.gameObject.SetActive(false);
                _poolObjects.Enqueue(item);
            }
        }

        public T GetObject()
        {
            if (_poolObjects.Count == 0)
            {
                CreateObject();
            }

            var item = _poolObjects.Dequeue();

            item.gameObject.SetActive(true);

            return item;
        }

        public void ReturnObject(T item)
        {
            if (item != null)
            {
                item.transform.SetParent(null, false);
                item.gameObject.SetActive(false);
                _poolObjects.Enqueue(item);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void ReaturnAllObject()
        {
            T[] items = Object.FindObjectsOfType<T>(false);

            foreach (var obj in items)
            {
                if (obj != null)
                {
                    ReturnObject(obj);
                }
            }
        }
        
        private void CreateObject()
        {
            if (_prefabs.Length == 0) return;

            var prefab = _prefabs[_currentIndex];
            var item = Instantiate(prefab);

            item.gameObject.SetActive(false);
            _poolObjects.Enqueue(item);


            _currentIndex = (_currentIndex + 1) % _prefabs.Length;
        }
    }
}