using UnityEngine;

[RequireComponent(typeof(CameraMover))]

public class MainCamera : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private CameraMover _mover;

    private void Awake()
    {
        _mover = GetComponent<CameraMover>();
    }

    private void Update()
    {
        _mover.Move(_inputReader.MousePosition);
    }
}
