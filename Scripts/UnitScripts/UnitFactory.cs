using UnityEngine;

public class UnitFactory : Factory<Unit>
{
    public override Unit Spawn(Vector3 spawnPosition, Transform parent)
    {
        Unit unit = Instantiate(Prefab, spawnPosition, Quaternion.identity);
        unit.gameObject.SetActive(false);
        unit.gameObject.SetActive(true);
        unit.transform.SetParent(parent);

        return unit;
    }
}
