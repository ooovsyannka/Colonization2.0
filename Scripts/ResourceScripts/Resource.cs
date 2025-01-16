using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]

public class Resource : MonoBehaviour, IPoolableObject
{
    [SerializeField] private float _timeToDie;

    private Coroutine _dieDelay;
    private WaitForSeconds _dieDelayWait;

    public event Action<IPoolableObject> Died;

    private void Awake()
    {
        _dieDelayWait = new WaitForSeconds(_timeToDie);
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
