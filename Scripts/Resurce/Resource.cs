using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]

public class Resource : MonoBehaviour, IPooledObject
{
    [SerializeField] private float _timeToDie;

    private Coroutine _dieDelay;
    private WaitForSeconds _dieDelayWait;

    public bool IsDetect { get; private set; }

    public event Action<IPooledObject> Died;

    private void Awake()
    {
        _dieDelayWait = new WaitForSeconds(_timeToDie);
    }

    private void OnEnable()
    {
        IsDetect = false;
    }

    public void Detect()
    {
        IsDetect = true;
    }

    public void Die()
    {
        if (_dieDelay != null)
            StopCoroutine(_dieDelay);

        _dieDelay = StartCoroutine(DieDelay());
    }

    private IEnumerator DieDelay()
    {
        yield return _dieDelayWait;

        gameObject.SetActive(false);
        Died?.Invoke(this);
    }
}
