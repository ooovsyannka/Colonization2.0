using UnityEngine;

public static class LayerCollision
{
    private static int _maxRayCastDistance = 150;

    public static bool IsDesiredLayerCollision(InputReader inputReader,LayerMask mask, out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputReader.MousePosition);

        return Physics.Raycast(ray, out hit, _maxRayCastDistance, mask);
    }

}