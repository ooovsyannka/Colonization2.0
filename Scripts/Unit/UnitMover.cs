using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _distance;

    private NavMeshAgent _agent;
    private Coroutine _moveTo;

    public event Action ArriveAtResurce;
    public event Action ArriveAtTower;

    public Vector3 BasePosition { get; private set; }
    public bool IsMove { get; private set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        BasePosition = transform.position;
    }

    public Coroutine MoveToResurce(Resource resource)
    {
        return StartMoveTo(resource.transform.position, ArriveAtResurce);
    }

    public Coroutine MoveToPlace()
    {
        return StartMoveTo(BasePosition, ArriveAtTower);
    }

    public Coroutine MoveToNewTower(TowerFlag tower)
    {
        return StartMoveTo(tower.transform.position);
    }

    private Coroutine StartMoveTo(Vector3 targetPosition, Action arriveAt = null)
    {
        if (_moveTo != null)
            StopCoroutine(_moveTo);

        return StartCoroutine(MoveTo(targetPosition, arriveAt));
    }

    private IEnumerator MoveTo(Vector3 targetPosition, Action arriveAt)
    {
        Move(targetPosition);

        while (IsArrived(targetPosition) == false)
        {
            yield return null;
        }

        StopMove();
        IsMove = false;

        arriveAt?.Invoke();
    }

    private void Move(Vector3 targetPosition)
    {
        IsMove = true;
        _agent.isStopped = false;

        _agent.SetDestination(targetPosition);
    }

    private void StopMove()
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
        IsMove = false;
    }

    private bool IsArrived(Vector3 targetPosition)
    {
        return transform.position.IsEnoughClose(targetPosition, _distance);
    }
}
