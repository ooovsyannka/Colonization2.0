using System;
using TMPro;
using UnityEngine;

public class TowerWarehouse : MonoBehaviour
{
    [SerializeField] private Tower _tower;

    [SerializeField] private TowerInfo _resourceInfo;

    private int _resurceCount;

    private void OnEnable()
    {
        _tower.ResourceReceived += AddResourceCount;
    }

    private void OnDisable()
    {
        _tower.ResourceReceived -= AddResourceCount;
    }

    public void Buy(int price)
    {
        _resurceCount -= price;
        _resourceInfo.ResourceCountUpdate(_resurceCount);
    }

    public bool CanBuy(int price)
    {
        if (_resurceCount >= price)
        {
            return true;
        }

        return false;
    }

    private void AddResourceCount(Resource _)
    {
        _resurceCount++;
        _resourceInfo.ResourceCountUpdate(_resurceCount);
    }
}
