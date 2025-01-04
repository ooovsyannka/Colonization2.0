using UnityEngine;

public class TowerObjectRenderer : MonoBehaviour
{
    [SerializeField] private Material _material;

    private Color _redColor = Color.red;
    private Color _grenColor = Color.green;

    private void OnEnable()
    {
        _material.color = _grenColor;
    }

    public void ChangeColorRed()
    {
        
            _material.color = _redColor;
        
    }
    public void ChangeColorGreen()
    {
        
            _material.color = _grenColor;
        
    }
}