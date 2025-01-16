using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnitHolder : MonoBehaviour
{
    [SerializeField] private List<Transform> _desiredPlaces;

    private UnitFabric _unitFabric;
    private Queue<Unit> _activeUnits;
    private Dictionary<Transform, bool> _unitPlaces;

    public int UnitCount {get; private set; }
    public bool HasActiveUnits { get { return _activeUnits.Count != 0; } }

    public event Action UnitReturned;

    private void Awake()
    {
        _activeUnits = new Queue<Unit>();
        _unitPlaces = new Dictionary<Transform, bool>();
    }

    private void Start()
    {
        foreach (Transform place in _desiredPlaces)
        {
            _unitPlaces.Add(place, false);
        }

        AddNewUnit();
    }

    public void FreeUpPlace(Unit unit)
    {
        foreach (KeyValuePair<Transform, bool> place in _unitPlaces)
        {
            if (unit.TryGetComponent(out UnitMover mover))
            {
                if (mover.BasePosition == place.Key.position)
                {
                    _unitPlaces[place.Key] = false;
                    UnitCount--;

                    return;
                }
            }
        }
    }

    public void SetUnitFabric(UnitFabric unitFabric)=>
        _unitFabric = unitFabric;

    public void AddNewUnit()
    {
        foreach (KeyValuePair<Transform, bool> place in _unitPlaces)
        {
            if (place.Value == false)
            {
                Unit unit = _unitFabric.Spawn(place.Key.position, null);
                _unitPlaces[place.Key] = true;
                _activeUnits.Enqueue(unit);
                UnitCount++;

                return;
            }
        }
    }

    public bool CanAddNewUnit()
    {
        foreach (KeyValuePair<Transform, bool> place in _unitPlaces)
        {
            if (place.Value == false)
                return true;
        }

        return false;
    }

    public bool TrySendActiveUnit(out Unit unit)
    {
        unit = null;

        if (TryGetActiveUnit(out unit))
        {
            unit.ResourceDelivered += ReturnUnit;

            return true;
        }

        return false;
    }

    public void ReturnUnit(Unit unit)
    {
        _activeUnits.Enqueue(unit);
        UnitReturned?.Invoke();
        unit.ResourceDelivered -= ReturnUnit;
    }

    public bool TryGetActiveUnit(out Unit desiredUnit)
    {
        desiredUnit = null;

        if (_activeUnits.Count > 0)
        {
            desiredUnit = _activeUnits.Dequeue();

            return true;
        }

        return false;
    }
}
