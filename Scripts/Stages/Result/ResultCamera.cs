using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルトのカメラをここで変更
/// </summary>
public class ResultCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera resultCamera;
    [SerializeField] private CinemachineVirtualCamera notEatResultCamera;
    private const int ACTIVE_PRIORITY = 20;
    private const int INACTIVE_PRIORITY = 10;

    private bool isRunning = false;

    void Awake()
    {
        if(resultCamera == null) { Debug.LogError("resultCamera"); return; }
        if(notEatResultCamera == null) { Debug.LogError("notEatResultCamera"); return; }
    }

    public void EatCamera()
    {
        if (isRunning) return;

        isRunning = true;
        resultCamera.Priority = ACTIVE_PRIORITY;
        notEatResultCamera.Priority = INACTIVE_PRIORITY;
    }

    public void NotEatCamera()
    {
        if(isRunning) return;

        isRunning = true;
        notEatResultCamera.Priority = ACTIVE_PRIORITY;
        resultCamera.Priority = INACTIVE_PRIORITY;
    }
}
