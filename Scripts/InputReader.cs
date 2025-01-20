using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action TrySelectedTower;

    public Vector3 MousePosition { get; private set; }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            TrySelectedTower();

        MousePosition = Input.mousePosition;
    }
}