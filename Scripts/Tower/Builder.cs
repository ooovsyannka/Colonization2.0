using System.Collections;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private TowerFabric _towerFabric;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _towerLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private int _maxRayCastDistance;

    private TowerObject _towerObject;
    private Tower _selectedTower;
    private Coroutine _setFalgCoroutine;
    private bool _canSetFlag = true;

    private void Update()
    {
        TryGetTower();
    }

    private void TryGetTower()
    {
        if (_inputReader.IsLeftMouseButton && _canSetFlag == true)
        {
            if (CheckRayCollision(_towerLayer, out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent(out Tower tower))
                {
                    _selectedTower = tower;
                    _towerObject = _selectedTower.SetTowerObject();

                    if (_setFalgCoroutine != null)
                    {
                        StopCoroutine(_setFalgCoroutine);
                    }

                    StartCoroutine(SetFlag());
                }
            }
        }
    }

    private IEnumerator SetFlag()
    {
        _canSetFlag = false;
        _towerObject.gameObject.SetActive(true);
        _selectedTower.UnitSendToNewTower -= SubscribeToUnitArrival;

        while (_canSetFlag == false)
        {
            if (_inputReader.IsLeftMouseButton)
            {
                if (_towerObject.CanBuild)
                {
                    _canSetFlag = true;
                }
            }

            if (CheckRayCollision(_groundLayer, out RaycastHit hit))
            {
                _towerObject.transform.position = hit.point;
            }

            yield return null;
        }

        _selectedTower.ChangePriority();
        _selectedTower.UnitSendToNewTower += SubscribeToUnitArrival;
    }

    private void BuildTower(Unit unit)
    {
        _towerFabric.Spawn(_towerObject.transform.position);
        _towerObject.gameObject.SetActive(false);
        _towerObject.BackToInitialPosition();
        unit.ArriveAtNewTower -= BuildTower;
    }

    private void SubscribeToUnitArrival(Unit unit)
    {
        unit.ArriveAtNewTower += BuildTower;
        _selectedTower.UnitSendToNewTower -= SubscribeToUnitArrival;
    }

    private bool CheckRayCollision(LayerMask mask, out RaycastHit hit)
    {
        Ray ray = _camera.ScreenPointToRay(_inputReader.MousePosition);

        return Physics.Raycast(ray, out hit, _maxRayCastDistance, mask);
    }
}