using UnityEngine;

public class TowerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Resource resource))
        {
           // resource.gameObject.;
        }
    }
}