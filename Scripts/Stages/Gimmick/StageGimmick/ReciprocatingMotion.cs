using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 指定された2つのポイントの間を、一定の速度で往復運動するギミック。
/// </summary>
public class ReciprocatingMotion : MonoBehaviour
{
    [Header("移動設定")]
    [Tooltip("移動する速さ")]
    [SerializeField] private float moveSpeed;

    [Header("移動範囲")]
    [Tooltip("移動の始点となるポイント")]
    [SerializeField] private Transform startPoint;
    [Tooltip("移動の終点となるポイント")]
    [SerializeField] private Transform finalPoint;

    [Header("判定設定")]
    [Tooltip("目的地に到達したと判定する距離")]
    [SerializeField] private float arrivalThreshold = 0.01f;

    private bool isHeadingToFinal = true;

    void Awake()
    {
        if(startPoint == null) { Debug.LogError("startPointが参照されていません"); return; }
        if(finalPoint == null) { Debug.LogError("finalPointが参照されていません"); return; }
    }

    void Update()
    {
        Move();
    }

    /// <summary>
    /// 往復運動の移動
    /// </summary>
    private void Move()
    {
        //現在向かうべき目的地をフラグから決定
        Transform _targetPoint = isHeadingToFinal ? finalPoint : startPoint;

        transform.position = Vector3.MoveTowards(
            transform.position,
            _targetPoint.position,
            moveSpeed * Time.deltaTime
         );

        if(Vector3.Distance(transform.position, _targetPoint.position) < arrivalThreshold)
        {
            isHeadingToFinal = !isHeadingToFinal;
        }
    }
}
