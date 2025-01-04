using UnityEngine;

public class MainCursor : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Texture2D _cursorTexture;

    private void Start()
    {
        Cursor.SetCursor(_cursorTexture, _inputReader.MousePosition, CursorMode.Auto);
    }
}