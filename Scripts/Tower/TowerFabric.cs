using UnityEngine;

public class TowerFabric : Fabric<Tower>
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private UnitFabric _unitFabric;
    [SerializeField] private TowerData _towerData;
    [SerializeField] private Vector3 _startSpawnPosition;

    private float _towerRotation = 180;

    private void Start()
    {
        Spawn(_startSpawnPosition);
    }

    public override Tower Spawn(Vector3 spawnPosition, Transform parent = null)
    {
        Tower tower = Instantiate(Prefab, spawnPosition, Quaternion.Euler(Vector3.up * _towerRotation));
        tower.GetUnitFabric(_unitFabric);
        _towerData.AddTowerResourceHolder(tower.SetTowerResourceHolder());

        if (tower.SetTowerFlag().TryGetComponent(out TowerFlagMover mover))
            mover.GetInputReader(_inputReader);

        return tower;
    }
}
