using UnityEngine;

public class TowerFlagRenderer : MonoBehaviour
{
    [SerializeField] private TowerFlag _flag;
    [SerializeField] private Material _material;

    private Color _redColor = Color.red;
    private Color _grenColor = Color.green;

    private void OnEnable()
    {
        _material.color = _grenColor;
        _flag.TriggerEnter += ChangeColorRed;
        _flag.TriggerExit += ChangeColorGreen;
    }

    private void OnDisable()
    {
        _flag.TriggerEnter -= ChangeColorRed;
        _flag.TriggerExit -= ChangeColorGreen;
    }

    private void ChangeColorRed()
    {
        _material.color = _redColor;
    }

    private void ChangeColorGreen()
    {
        _material.color = _grenColor;
    }
}