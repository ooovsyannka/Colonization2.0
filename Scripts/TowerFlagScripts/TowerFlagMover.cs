using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LayerCollision))]

public class TowerFlagMover : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;

    private int _maxRayCastDistance = 500;
    private LayerCollision _layerCollision;
    private InputReader _reader;
    private Coroutine _move;

    private void Awake()
    {
        _layerCollision = GetComponent<LayerCollision>();
    }

    public void SetInputReader(InputReader reader) =>
        _reader = reader;

    public void StartMove()
    {
        StopMove();

        _move = StartCoroutine(Move());
    }

    public void StopMove()
    {
        if (_move != null)
            StopCoroutine(_move);
    }

    private IEnumerator Move()
    {
        bool isWork = false;

        while (isWork == false)
        {
            if (_reader != null)
            {
                if (_layerCollision.IsDesiredLayerCollision(_reader, _maxRayCastDistance, _groundLayer, out RaycastHit hit))
                {
                    transform.position = hit.point;
                }
            }

            yield return null;
        }
    }
}
