using System.Collections.Generic;
using UnityEngine;

public class TowerData : MonoBehaviour
{
    private List<TowerResourceHolder> _resourceHolders;

    private void Awake()
    {
        _resourceHolders = new List<TowerResourceHolder>();
    }

    public void AddTowerResourceHolder(TowerResourceHolder resourceHolder)
    {
        _resourceHolders.Add(resourceHolder);
        resourceHolder.SetTowerData(this);
    }

    public bool CanAddResource(Resource resource)
    {
        foreach (TowerResourceHolder resourceHolder in _resourceHolders)
        {
            if (resourceHolder.HasCurrentResource(resource))
                return false;
        }

        return true;
    }
}