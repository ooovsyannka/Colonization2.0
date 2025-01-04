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
    private Tower _selectTower;
    private Coroutine _setFalgCoroutine;
    private float _setFlagDelayTime = 0.05f;
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
                    if (tower.UnitCount > 1)
                    {
                        _selectTower = tower;
                        _towerObject = _selectTower.SetTowerObject();

                        if (_setFalgCoroutine != null)
                        {
                            StopCoroutine(_setFalgCoroutine);
                        }

                        StartCoroutine(SetFlag());
                    }
                    else
                    {
                        tower.ShowErrorMesange();
                    }
                }
            }
        }
    }

    private IEnumerator SetFlag()
    {
        _canSetFlag = false;
        _towerObject.gameObject.SetActive(true);
        _selectTower.UnitSendToNewTower -= GetUnit;

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

        _selectTower.ChangePriority();
        _selectTower.UnitSendToNewTower += GetUnit;
    }

    private void CanBuildTower(Unit unit)
    {
        _towerFabric.Spawn(_towerObject.transform.position);
        _towerObject.gameObject.SetActive(false);
        _towerObject.BackToInitialPosition();
        unit.ArriveAtNewTower -= CanBuildTower;
    }

    private void GetUnit(Unit unit)
    {
        unit.ArriveAtNewTower += CanBuildTower;
        _selectTower.UnitSendToNewTower -= GetUnit;
    }

    private bool CheckRayCollision(LayerMask mask, out RaycastHit hit)
    {
        Ray ray = _camera.ScreenPointToRay(_inputReader.MousePosition);

        return Physics.Raycast(ray, out hit, _maxRayCastDistance, mask);
    }
}