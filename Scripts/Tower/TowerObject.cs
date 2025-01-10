using UnityEngine;

public class TowerObject : MonoBehaviour
{
    [SerializeField] private TowerObjectRenderer _objectRenderer;

    private Vector3 _initialPosition;

    public bool IsUse { get; private set; }
    public bool CanBuild { get; private set; }

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void OnEnable()
    {
        CanBuild = false;
        IsUse = true;
    }

    private void OnDisable()
    {
        IsUse = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Tower _) || other.TryGetComponent(out TowerObject _))
        {
            CanBuild = false;
            _objectRenderer.ChangeColorRed();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Tower _) || other.TryGetComponent(out TowerObject _))
        {
            CanBuild = true;
            _objectRenderer.ChangeColorGreen();
        }
    }

    public void BackToInitialPosition()
    {
        _initialPosition = transform.position;
    }
}
