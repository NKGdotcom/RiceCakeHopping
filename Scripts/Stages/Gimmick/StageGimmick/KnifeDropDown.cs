using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一定間隔で自由落下し、自動的に元の位置に戻る障害物のギミック
/// </summary>
public class KnifeDropDown : MonoBehaviour
{
    [Header("落下設定")]
    [Tooltip("何秒間隔で包丁を落とすか")]
    private float fallInterval = 3;

    //状態
    private Vector3 initialPos;
    private float timer;
    private Rigidbody knifeRigidbody;

    private void Awake()
    {
        timer = fallInterval;
        initialPos = transform.position;
        knifeRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            //時間が来たら元の位置に戻し、タイマーリセット
            ResetPosition();
            timer = fallInterval;
        }
    }

    /// <summary>
    /// 包丁を初期位置に戻し、落下の勢いを完全に殺す
    /// </summary>
    private void ResetPosition()
    {
        //位置を戻す
        this.transform.position = initialPos;

        //落下の勢いをリセット
        knifeRigidbody.velocity = Vector3.zero;
        knifeRigidbody.angularVelocity = Vector3.zero;
    }
}
