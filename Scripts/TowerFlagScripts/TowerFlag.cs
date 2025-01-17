using System;
using UnityEngine;

[RequireComponent(typeof(TowerFlagMover))]

public class TowerFlag : MonoBehaviour
{
    private Vector3 _initialPosition;
    private TowerFlagMover _mover;

    public bool CanBuild { get; private set; }

    public event Action TriggerEnter;
    public event Action TriggerExit;

    private void Awake()
    {
        _initialPosition = transform.position;
        _mover = GetComponent<TowerFlagMover>();
    }

    private void OnEnable()
    {
        CanBuild = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Tower _) || other.TryGetComponent(out TowerFlag _))
        {
            CanBuild = false;
            TriggerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Tower _) || other.TryGetComponent(out TowerFlag _))
        {
            CanBuild = true;
            TriggerExit?.Invoke();
        }
    }

    public void BackToInitialPosition()
    {
        _initialPosition = transform.position;
    }

    public void StartMove() =>
        _mover.StartMove();

    public void StopMove() =>
        _mover.StopMove();
}
