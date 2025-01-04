using System;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour, IFabricObject
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private TowerUnitHolder _unitHolder;
    [SerializeField] private TowerResourceHolder _resourceHolder;
    [SerializeField] private TowerWarehouse _warehouse;
    [SerializeField] private TowerBuyer _buyer;
    [SerializeField] private TowerObject _object;
    [SerializeField] private TowerInfo _info;

    private bool _isBuildNewTower;
    private bool _canBuildNewTower;

    public int UnitCount { get { return _unitHolder.IndexUnit; } }

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
        _unitHolder.GetUnitFabric(unitFabric);
    }

    public TowerObject SetTowerObject() => _object;

    public void ChangePriority()
    {
        _isBuildNewTower = true;
    }

    public void GetErrorMessage(TextMeshProUGUI errorMessage)
    {
        _info.GetErrorMessage(errorMessage);
    }

    public void ShowErrorMesange()
    {
        _info.ShowErrorMessageLackUnit();
    }

    private void TryAddResurce(Resource detectedResource)
    {
        if (_resourceHolder.CanAddResurce(detectedResource))
        {
            _resourceHolder.AddResurce(detectedResource);
            TrySendUnit();
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
            unit.StartMoveToTower(_object);
            _isBuildNewTower = false;
        }
    }

    private void GetResource(Resource resource, Unit unit)
    {
        ResourceReceived?.Invoke(resource);
        unit.ResourceUnloaded -= GetResource;

        PurchasePriority();
    }

    private void PurchasePriority()
    {
        if (_isBuildNewTower == false)
        {
            _buyer.TryBuyNewUnit();
        }
        else
        {
            if (_buyer.CanBuyNewTower())
            {
                _canBuildNewTower = true;
            }
        }
    }
}
