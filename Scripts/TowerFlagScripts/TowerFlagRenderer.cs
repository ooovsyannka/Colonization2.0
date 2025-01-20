using UnityEngine;

public class TowerFlagRenderer : MonoBehaviour
{
    [SerializeField] private TowerFlag _flag;
    [SerializeField] private Material _material;

    private Color _buildDeniedColor = Color.red;
    private Color _buildAllowedColor = Color.green;

    private void OnEnable()
    {
        _material.color = _buildAllowedColor;
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
        _material.color = _buildDeniedColor;
    }

    private void ChangeColorGreen()
    {
        _material.color = _buildAllowedColor;
    }
}