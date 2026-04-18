using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 残り時間を計算し、UIに表示するクラス
/// </summary>
public class TimeView : MonoBehaviour
{
    [Header("UI参照")]
    [Tooltip("時間を表示するTMP")]
    [SerializeField] private TextMeshProUGUI timeTMP;

    private void Awake()
    {
        if(timeTMP == null) { Debug.LogError("timeTMPが参照されていません"); return; }
    }

    /// <summary>
    /// 渡された秒数を「分・秒」に変換してテキストを更新
    /// </summary>
    /// <param name="_timer"></param>
    public void UpdateTMP(float _timer)
    {
        //UI上でマイナスにならないようにする
        float _currentTimer = Mathf.Max(0, _timer);

        //分と秒を計算
        int _minutes = Mathf.FloorToInt(_currentTimer / 60);
        int _seconds = Mathf.FloorToInt(_currentTimer % 60);

        //テキストを更新
        timeTMP.text = string.Format("{0}:{1:00}", _minutes, _seconds);
    }
}
