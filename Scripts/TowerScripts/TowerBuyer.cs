using UnityEngine;

public class TowerBuyer : MonoBehaviour
{
    private const int AllowedNumberUnit = 1;

    [SerializeField] private TowerUnitHolder _unitHolder;
    [SerializeField] private TowerWarehouse _warehouse;

    private bool _isTowerPriority;
    private bool _isFlagSet;
    private int _unitPrice = 3;
    private int _towerPrice = 5;

    public void AttemptPurchaseUnit()
    {
        if (_isTowerPriority == false)
        {
            if (_unitHolder.CanAddNewUnit())
            {
                if (_warehouse.CanBuy(_unitPrice))
                {
                    _warehouse.Buy(_unitPrice);
                    _unitHolder.AddNewUnit();
                }
            }
        }
    }

    public bool CanBuyTower()
    {
        if (_unitHolder.UnitCount > AllowedNumberUnit)
        {
            if (_isFlagSet)
            {
                if (_warehouse.CanBuy(_towerPrice))
                {
                    _warehouse.Buy(_towerPrice);
                    _isTowerPriority = false;

                    return true;
                }

                _isTowerPriority = true;
            }
        }

        return false;
    }

    public void OnFlagSet() => _isFlagSet = true;

    public void OffFlagSet() => _isFlagSet = false;
}