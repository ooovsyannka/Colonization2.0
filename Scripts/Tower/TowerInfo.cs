using TMPro;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resurceCountText;

    public void ResourceCountUpdate(int resourceCount)
    {
        _resurceCountText.text = resourceCount.ToString();
    }
}