using System.Collections;
using UnityEngine;

public class TowerFlagMover : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;

    private InputReader _reader;
    private Coroutine _move;

    public void GetInputReader(InputReader reader) => _reader = reader;

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
                if (LayerCollision.IsDesiredLayerCollision(_reader, _groundLayer, out RaycastHit hit))
                {
                    transform.position = hit.point;
                }
            }

            yield return null;
        }
    }
}
