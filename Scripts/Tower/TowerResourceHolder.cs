using System.Collections.Generic;
using UnityEngine;

public class TowerResourceHolder : MonoBehaviour
{
    [SerializeField] private Tower _tower;

    private Dictionary<Resource, bool> _resurces;
    private int _maxResourceCount = 3;

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

    public bool CanAddResurce(Resource detectedResource)
    {
        if (_resurces.Count > _maxResourceCount)
            return false;

        if (detectedResource.IsDetect)
            return false;

        if (_resurces.ContainsKey(detectedResource))
            return false;

        return true;
    }

    public void AddResurce(Resource detectedResource)
    {
        detectedResource.Detect();

        _resurces.Add(detectedResource, false);
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