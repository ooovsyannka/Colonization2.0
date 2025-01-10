using System;
using System.Collections;
using UnityEngine;

public class UnitTrailer : MonoBehaviour
{
    [SerializeField] private UnitAnimation _animation;
    [SerializeField] private Transform _resurseParent;

    private Coroutine _processingDelay;
    private WaitForSeconds _processingWait;
    private WaitForSeconds _halfProcessingWait;
    private Resource _resource;

    private void Awake()
    {
        int divisionAnimationLength = 2;

        _processingWait = new WaitForSeconds(_animation.ProcessingAnimationDuration.length);
        _halfProcessingWait = new WaitForSeconds(_animation.ProcessingAnimationDuration.length / divisionAnimationLength);
    }

    public Coroutine UploadResurce(Resource resource)
    {
        _resource = resource;

        return StartProcessingCoroutine(_resurseParent);
    }

    public Coroutine UnloadResurce(Action<Resource, Unit> resourceUnloaded, Unit unit)
    {
        return StartProcessingCoroutine(null, resourceUnloaded, unit);
    }

    private Coroutine StartProcessingCoroutine(Transform parentPosition, Action<Resource, Unit> resourceUnloaded = null, Unit unit = null)
    {
        if (_processingDelay != null)
            StopCoroutine(_processingDelay);

        return StartCoroutine(ProcessingAnimationDelay(parentPosition, resourceUnloaded, unit));
    }

    private IEnumerator ProcessingAnimationDelay(Transform parentPosition, Action<Resource, Unit> resourceUnloaded, Unit unit)
    {
        yield return _halfProcessingWait;

        _resource.transform.parent = parentPosition;

        if (parentPosition != null)
        {
            _resource.transform.localPosition = Vector3.zero;
        }
        else
        {
            resourceUnloaded?.Invoke(_resource, unit);
        }

        yield return _processingWait;
    }
}