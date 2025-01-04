using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> _pool = new Queue<T>();
    private T _prefab;

    public ObjectPool(T prefab)
    {
        _prefab = prefab;
    }

    public T GetObject()
    {
        if (_pool.Count > 0)
        {
            return _pool.Dequeue();
        }

        return CrateObject();
    }

    public void PutObject(T obj)
    {
        _pool.Enqueue(obj);
    }

    private T CrateObject()
    {
        T currentObject = Object.Instantiate(_prefab);
        currentObject.gameObject.SetActive(false);

        return currentObject;
    }
}