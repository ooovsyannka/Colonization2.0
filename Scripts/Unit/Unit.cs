using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UnitMover))]

public class Unit : MonoBehaviour, IFabricObject
{
    [SerializeField] private UnitTrailer _trailer;
    [SerializeField] private UnitAnimation _animation;

    private UnitMover _mover;

    public event Action<Unit> ResourceDelivered;
    public event Action<Resource, Unit> ResourceUnloaded;
    public event Action<Unit> ArriveAtNewTower;

    private void Awake()
    {
        _mover = GetComponent<UnitMover>();
    }

    private void Update()
    {
        _animation.PlayAnimation(_mover.IsMove);
    }

    public void StartDeliveryResource(Resource resource)
    {
        StartCoroutine(DeliveryResource(resource));
    }

    public void StartMoveToTower(TowerObject tower)
    {
        StartCoroutine(MoveToTower(tower));
    }

    private IEnumerator MoveToTower(TowerObject tower)
    {
        yield return _mover.MoveToNewTower(tower);

        ArriveAtNewTower?.Invoke(this);
        Destroy(gameObject);
    }

    private IEnumerator DeliveryResource(Resource resource)
    {
        yield return _mover.MoveToResurce(resource);
        
        _animation.PlayProcessing();

        yield return _trailer.UploadResurce(resource);

        yield return _mover.MoveToPlace();

        _animation.PlayProcessing();
        
        yield return _trailer.UnloadResurce(ResourceUnloaded, this);

        resource.Die(); 
        ResourceDelivered?.Invoke(this);
    }
}
