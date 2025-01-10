using UnityEngine;

public class TimeSwitcher : MonoBehaviour
{
  private  bool _switchTime;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _switchTime = !_switchTime;
        }

       if (_switchTime)
        {
            Time.timeScale = 5;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}