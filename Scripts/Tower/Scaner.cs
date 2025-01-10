using System;
using System.Collections;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] private Vector3 _centrPosition;
    [SerializeField] private Vector3 _scale;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _scanDelay;

    private WaitForSeconds _scanWait;

    public event Action<Resource> ResourceDetected;

    private void Awake()
    {
        _scanWait = new WaitForSeconds(_scanDelay);
    }

    private void Start()
    {
        StartCoroutine(Scanning());
    }

    private IEnumerator Scanning()
    {
        while (enabled)
        {
            Collider[] colliders = Physics.OverlapBox(_centrPosition, _scale, Quaternion.identity, _layerMask);

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out Resource resource))
                {
                    ResourceDetected?.Invoke(resource);
                }
            }

            yield return _scanWait;
        }
    }
}
