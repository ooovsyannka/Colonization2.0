using UnityEngine;

public abstract class Fabric<T> : MonoBehaviour where T : MonoBehaviour, IFabricObject
{
    [field : SerializeField] public T Prefab{ get; private set; }

    public abstract T Spawn(Vector3 spawnPosition, Transform parent = null);
}
