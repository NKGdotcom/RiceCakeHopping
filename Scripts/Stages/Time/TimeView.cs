using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 時間制限をテキストに反映
/// </summary>
public class TimeView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeTMP;

    private void Awake()
    {
        if(timeTMP == null) { TryGetComponent<TextMeshProUGUI>(out timeTMP); }
    }

    public void UpdateTMP(float _timer)
    {
        float _currentTimer = Mathf.Max(0, _timer);

        int _minutes = Mathf.FloorToInt(_currentTimer / 60);
        int _seconds = Mathf.FloorToInt(_currentTimer % 60);

        timeTMP.text = string.Format("{0}:{1:00}", _minutes, _seconds);
    }
}
