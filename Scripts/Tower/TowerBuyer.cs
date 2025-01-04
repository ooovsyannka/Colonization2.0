using UnityEngine;

public class TowerBuyer : MonoBehaviour
{
    [SerializeField] private TowerUnitHolder _unitHolder;
    [SerializeField] private TowerWarehouse _warehouse;

    private int _unitPrice = 3;
    private int _towerPrice = 5;

    public void TryBuyNewUnit()
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

    public bool CanBuyNewTower()
    {
        if (_warehouse.CanBuy(_towerPrice))
        {
            _warehouse.Buy(_towerPrice);

            return true;
        }

        return false;
    }
}