using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner<T> where T : MonoBehaviour, IPooledObject
{
    private ObjectPool<T> _pool;
    private List<T> _activeObjects = new List<T>();

    public Spawner(T prefab)
    {
        _pool = new ObjectPool<T>(prefab);
    }

    public T Spawn(Vector3 spawnPosition)
    {
        T currentObject = _pool.GetObject();
        _activeObjects.Add(currentObject);
        currentObject.Died += ReturnObjectInPool;
        currentObject.transform.position = spawnPosition;

        return currentObject;
    }

    public void CleanActiveObject()
    {
        if (_activeObjects.Count > 0)
        {
            for (int i = _activeObjects.Count - 1; i >= 0; i--)
            {
                T currentObject = _activeObjects[i];

                currentObject.gameObject.SetActive(false);
                ReturnObjectInPool(currentObject);
            }
        }
    }

    private void ReturnObjectInPool(IPooledObject returnedObject)
    {
        _activeObjects.Remove((T)returnedObject);
        _pool.PutObject((T)returnedObject);
        returnedObject.Died -= ReturnObjectInPool;
    }
}