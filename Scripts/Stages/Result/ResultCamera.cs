using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルト時のカメラ演出を管理
/// </summary>
public class ResultCamera : MonoBehaviour
{
    [Header("演出")]
    [Tooltip("通常のリザルトカメラ")]
    [SerializeField] private CinemachineVirtualCamera resultCamera;
    [Tooltip("食えなかった時のリザルトカメラ")]
    [SerializeField] private CinemachineVirtualCamera notEatResultCamera;

    //優先度
    private const int ACTIVE_PRIORITY = 20;
    private const int INACTIVE_PRIORITY = 10;

    //演出が走ったか
    private bool isRunning = false;

    void Awake()
    {
        if(resultCamera == null) { Debug.LogError("resultCamera"); return; }
        if(notEatResultCamera == null) { Debug.LogError("notEatResultCamera"); return; }
    }

    /// <summary>
    /// 何か食べることができた際(通常演出)のカメラ遷移
    /// </summary>
    public void EatCamera()
    {
        if (isRunning) return;

        //フラグをオン
        isRunning = true;

        //通常演出のカメラに滑らかに切り替える
        resultCamera.Priority = ACTIVE_PRIORITY;

        //元々のカメラの優先度を一応下げる
        notEatResultCamera.Priority = INACTIVE_PRIORITY;
    }

    /// <summary>
    /// 何も食べられなかった際のカメラ遷移
    /// </summary>
    public void NotEatCamera()
    {
        if(isRunning) return;

        //フラグをオン
        isRunning = true;

        //食べられなかったカメラに滑らかに切り替える
        notEatResultCamera.Priority = ACTIVE_PRIORITY;

        //元々のカメラの優先度を一応下げる
        resultCamera.Priority = INACTIVE_PRIORITY;
    }
}
