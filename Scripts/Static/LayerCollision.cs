using UnityEngine;

public class LayerCollision : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public bool IsDesiredLayerCollision(InputReader inputReader, int maxRayCastDistance, LayerMask mask, out RaycastHit hit)
    {
        Ray ray = _mainCamera.ScreenPointToRay(inputReader.MousePosition);

        return Physics.Raycast(ray, out hit, maxRayCastDistance, mask);
    }

}