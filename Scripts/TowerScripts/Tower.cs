using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IFactoryObject
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private TowerUnitHolder _unitHolder;
    [SerializeField] private TowerResourceHolder _resourceHolder;
    [SerializeField] private TowerWarehouse _warehouse;
    [SerializeField] private TowerBuyer _buyer;
    [SerializeField] private TowerFlag _flag;
    [SerializeField] private TowerInfo _info;

    private bool _canBuildNewTower;

    public event Action<Resource> ResourceReceived;
    public event Action<Unit> UnitSendToNewTower;

    private void OnEnable()
    {
        _scaner.ResourceDetected += TryAddResurce;
        _unitHolder.UnitReturned += TrySendUnit;
    }

    private void OnDisable()
    {
        _scaner.ResourceDetected -= TryAddResurce;
        _unitHolder.UnitReturned -= TrySendUnit;
    }

    public void GetUnitFabric(UnitFabric unitFabric)
    {
        _unitHolder.SetUnitFabric(unitFabric);
    }

    public TowerFlag SetTowerFlag() => _flag;

    public TowerResourceHolder SetTowerResourceHolder() => _resourceHolder;

    public void ChangePriority()
    {
        _buyer.OnFlagSet();
    }

    private void TryAddResurce(List<Resource> detectedResource)
    {
        foreach (Resource resource in detectedResource)
        {
            if (_resourceHolder.CanAddResurce(resource))
            {
                TrySendUnit();
            }
        }
    }

    private void TrySendUnit()
    {
        if (_unitHolder.HasActiveUnits)
        {
            if (_canBuildNewTower == false)
            {
                SendUnitToResource();
            }
            else
            {
                SendUnitToNewTower();
            }
        }
    }

    private void SendUnitToResource()
    {
        if (_resourceHolder.TryGetFreeResurce(out Resource resource))
        {
            if (_unitHolder.TrySendActiveUnit(out Unit unit))
            {
                unit.StartDeliveryResource(resource);
                unit.ResourceUnloaded += GetResource;
            }
        }
    }

    private void SendUnitToNewTower()
    {
        if (_unitHolder.TryGetActiveUnit(out Unit unit))
        {
            _canBuildNewTower = false;
            UnitSendToNewTower?.Invoke(unit);
            _unitHolder.FreeUpPlace(unit);
            unit.StartMoveToTower(_flag);
            _buyer.OffFlagSet();
        }
    }

    private void GetResource(Resource resource, Unit unit)
    {
        ResourceReceived?.Invoke(resource);
        unit.ResourceUnloaded -= GetResource;

        SelectPriority();
    }

    private void SelectPriority()
    {
        if (_buyer.CanBuyNewTower())
        {
            _canBuildNewTower = true;
        }
        else
        {
            _buyer.TryBuyNewUnit();
        }
    }
}
