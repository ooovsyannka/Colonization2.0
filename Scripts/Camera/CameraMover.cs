using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _speed = 20;
    [SerializeField] private float _borderScreen;

    private Vector2 _screenCenter;
    private Vector3 _moveDirection;
    private float _screenWidthCenter;
    private float _screenHeightCenter;

    private void Awake()
    {
        int dividingBorderScreen = 2;

        _screenWidthCenter = Screen.width / dividingBorderScreen;
        _screenHeightCenter = Screen.height / dividingBorderScreen;
        _screenCenter = new Vector2(_screenWidthCenter, _screenHeightCenter);
    }

    public void Move(InputReader inputReader)
    {
        float horizontalDistance = inputReader.MousePosition.x - _screenCenter.x;
        float verticalDistance = inputReader.MousePosition.y - _screenCenter.y;

        _moveDirection.x = Mathf.Abs(horizontalDistance) >= _screenWidthCenter - _borderScreen ? Mathf.Sign(horizontalDistance) : 0;
        _moveDirection.z = Mathf.Abs(verticalDistance) >= _screenHeightCenter - _borderScreen ? Mathf.Sign(verticalDistance) : 0;

        transform.Translate(_moveDirection * _speed * Time.deltaTime, Space.Self);
    }
}