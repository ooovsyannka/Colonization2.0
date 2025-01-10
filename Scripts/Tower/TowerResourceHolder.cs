using System.Collections.Generic;
using UnityEngine;

public class TowerResourceHolder : MonoBehaviour
{
    [SerializeField] private Tower _tower;

    private TowerData _data;
    private Dictionary<Resource, bool> _resurces;

    private void Awake()
    {
        _resurces = new Dictionary<Resource, bool>();
    }

    private void OnEnable()
    {
        _tower.ResourceReceived += RemoveResource;
    }

    private void OnDisable()
    {
        _tower.ResourceReceived -= RemoveResource;
    }

    public void GetTowerData(TowerData data) => _data = data;

    public bool HasCurrentResource(Resource detectedResource)
    {
        if (_resurces.ContainsKey(detectedResource))
            return true;

        return false;
    }

    public bool CanAddResurce(Resource detectedResource)
    {
        if (_data.Test(detectedResource))
        {
            _resurces.Add(detectedResource, false);
            
            return true;
        }

        return false;
    }

    public bool TryGetFreeResurce(out Resource desiredResource)
    {
        desiredResource = null;

        if (_resurces.Count > 0)
        {
            foreach (KeyValuePair<Resource, bool> resource in _resurces)
            {
                if (resource.Value == false)
                {
                    desiredResource = resource.Key;
                    _resurces[desiredResource] = true;

                    return true;
                }
            }
        }

        return false;
    }

    private void RemoveResource(Resource resource)
    {
        _resurces.Remove(resource);
    }
}