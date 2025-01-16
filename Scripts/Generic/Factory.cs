using UnityEngine;

public abstract class Factory<T> : MonoBehaviour where T : MonoBehaviour, IFactoryObject
{
    [field : SerializeField] public T Prefab{ get; private set; }

    public abstract T Spawn(Vector3 spawnPosition, Transform parent = null);
}
