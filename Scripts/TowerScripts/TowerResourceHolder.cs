using System.Collections.Generic;
using UnityEngine;

public class TowerResourceHolder : MonoBehaviour
{
    private const int MaxCountResource = 3;

    private TowerData _data;
    private Dictionary<Resource, bool> _resources;

    private void Awake()
    {
        _resources = new Dictionary<Resource, bool>();
    }

    public void SetTowerData(TowerData data) => 
        _data = data;

    public bool HasCurrentResource(Resource detectedResource) => 
        _resources.ContainsKey(detectedResource);

    public bool CanAddResurce(Resource detectedResource)
    {
        if (_resources.Count < MaxCountResource)
        {
            if (_data.CanAddResource(detectedResource))
            {
                _resources.Add(detectedResource, false);
                detectedResource.Died += RemoveResource;

                return true;
            }
        }

        return false;
    }

    public bool TryGetFreeResurce(out Resource desiredResource)
    {
        desiredResource = null;

        if (_resources.Count > 0)
        {
            foreach (KeyValuePair<Resource, bool> resource in _resources)
            {
                if (resource.Value == false)
                {
                    desiredResource = resource.Key;
                    _resources[desiredResource] = true;

                    return true;
                }
            }
        }

        return false;
    }

    private void RemoveResource(IPoolableObject resource)
    {
        _resources.Remove((Resource)resource);
    }
}