using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resurceCountText;
    [SerializeField] private float _smoothlyShowMassageDelay;

    private TextMeshProUGUI _errorMessage;
    private Coroutine _smoothlyShowMassage;

    public void ResourceCountUpdate(int resourceCount)
    {
        _resurceCountText.text = resourceCount.ToString();
    }

    public void GetErrorMessage(TextMeshProUGUI text)
    {
        _errorMessage = text;
        _errorMessage.color = Color.clear;
    }

    public void ShowErrorMessageLackUnit()
    {
        string message = "НЕ ХВАТАЕТ ЮНИТОВ!!!";

        if (_smoothlyShowMassage != null)
            StopCoroutine(_smoothlyShowMassage);

        _smoothlyShowMassage = StartCoroutine(SmoothlyShowMassage(message));
    }

    private IEnumerator SmoothlyShowMassage(string message)
    {
        _errorMessage.text = message;
        float elapsedTime = 0;

        _errorMessage.color = Color.red;

        while (_errorMessage.alpha != 0)
        {
            _errorMessage.color = Color.Lerp(_errorMessage.color, Color.clear, elapsedTime / _smoothlyShowMassageDelay);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}