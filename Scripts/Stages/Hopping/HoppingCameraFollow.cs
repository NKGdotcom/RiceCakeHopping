using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングにぴったりと追従するカメラの制御クラスのカメラの挙動
/// </summary>
public class HoppingCameraFollow : MonoBehaviour
{
    [Header("追従設定")]
    [Tooltip("カメラが追いかける対象")]
    [SerializeField] private Transform hopping;

    //ターゲットとカメラの初期位置の差
    private Vector3 cameraOffset;

    private void Awake()
    {
        if(hopping == null) { Debug.LogError("hoppingが参照されていません"); return; }
    }

    void Start()
    {
        //ゲーム開始時のターゲットとカメラの距離を記憶しておく
        cameraOffset = transform.position - hopping.transform.position;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    /// <summary>
    /// ターゲットの位置にオフセットを足して、カメラの位置を合わせる
    /// </summary>
    private void FollowTarget() => transform.position = hopping.transform.position + cameraOffset;
}
