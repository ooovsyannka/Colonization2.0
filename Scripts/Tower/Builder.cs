using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private TowerFabric _towerFabric;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _towerLayer;
    [SerializeField] private LayerMask _groundLayer;

    private TowerFlag _towerFlag;
    private Tower _selectedTower;

    private void OnEnable()
    {
        _inputReader.LeftMouseButtonClicked += TryGetTower;
    }

    private void OnDisable()
    {
        _inputReader.LeftMouseButtonClicked -= SetFlag;
        _inputReader.LeftMouseButtonClicked -= TryGetTower;
    }

    private void TryGetTower()
    {
        if (LayerCollision.IsDesiredLayerCollision(_inputReader, _towerLayer, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out Tower tower))
            {
                _selectedTower = tower;
                _towerFlag = _selectedTower.SetTowerFlag();
                _towerFlag.gameObject.SetActive(true);
                _towerFlag.StartMove();

                _inputReader.LeftMouseButtonClicked -= TryGetTower;
                _inputReader.LeftMouseButtonClicked += SetFlag;
            }
        }
    }

    private void SetFlag()
    {
        if (_towerFlag.CanBuild)
        {
            _selectedTower.ChangePriority();
            _selectedTower.UnitSendToNewTower -= SubscribeToUnitArrival;
            _selectedTower.UnitSendToNewTower += SubscribeToUnitArrival;
            _towerFlag.StopMove();

            _inputReader.LeftMouseButtonClicked -= SetFlag;
            _inputReader.LeftMouseButtonClicked += TryGetTower;
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