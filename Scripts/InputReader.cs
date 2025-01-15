using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public Vector3 MousePosition { get; private set; }

    public event Action LeftMouseButtonClicked;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            LeftMouseButtonClicked();

        MousePosition = Input.mousePosition;
    }
}
