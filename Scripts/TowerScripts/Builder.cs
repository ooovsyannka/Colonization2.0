using UnityEngine;

[RequireComponent(typeof(LayerCollision))]

public class Builder : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private TowerFactory _towerFabric;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _towerLayer;
    [SerializeField] private LayerMask _groundLayer;

    private int _maxRayCastDistance = 50;
    private LayerCollision _layerCollision;
    private TowerFlag _towerFlag;
    private Tower _selectedTower;

    private void Awake()
    {
        _layerCollision = GetComponent<LayerCollision>();
    }

    private void OnEnable()
    {
        _inputReader.TrySelectedTower += SelectTower;
    }

    private void OnDisable()
    {
        _inputReader.TrySelectedTower -= CompleteTowerPlacement;
        _inputReader.TrySelectedTower -= SelectTower;
    }

    private void SelectTower()
    {
        if (_layerCollision.IsDesiredLayerCollision(_inputReader, _maxRayCastDistance, _towerLayer, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out Tower tower))
            {
                _selectedTower = tower;
                _towerFlag = _selectedTower.SetTowerFlag();
                _towerFlag.gameObject.SetActive(true);
                _towerFlag.StartMove();

                _inputReader.TrySelectedTower -= SelectTower;
                _inputReader.TrySelectedTower += CompleteTowerPlacement;
            }
        }
    }

    private void CompleteTowerPlacement()
    {
        if (_towerFlag.CanBuild)
        {
            _selectedTower.ChangePriority();
            _selectedTower.UnitSendToNewTower -= SubscribeToUnitArrival;
            _selectedTower.UnitSendToNewTower += SubscribeToUnitArrival;
            _towerFlag.StopMove();

            _inputReader.TrySelectedTower -= CompleteTowerPlacement;
            _inputReader.TrySelectedTower += SelectTower;
        }
    }

    private void BuildTower(Unit unit)
    {
        _towerFabric.Spawn(_towerFlag.transform.position);
        _towerFlag.gameObject.SetActive(false);
        _towerFlag.BackToInitialPosition();
        unit.ArriveAtNewTower -= BuildTower;
    }

    private void SubscribeToUnitArrival(Unit unit)
    {
        unit.ArriveAtNewTower += BuildTower;
        _selectedTower.UnitSendToNewTower -= SubscribeToUnitArrival;
    }
}