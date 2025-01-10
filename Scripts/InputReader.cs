using UnityEngine;

public class InputReader : MonoBehaviour 
{
    public bool IsRightMouseButton { get { return Input.GetMouseButtonUp(1); } }
    public bool IsLeftMouseButton { get { return Input.GetMouseButtonUp(0); } }
    public Vector3 MousePosition { get { return Input.mousePosition; } }
}
