using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングの足元に影を設置
/// 距離感などを図り、操作しやすくする
/// </summary>
public class HoppingShadowFeet : MonoBehaviour
{
    [SerializeField] private Transform hoppingIdleTransform;
    private float shadowIdlePosY;
    private float shadowScale;
    private float shadowMagAdjustment = 0.5f; //影の倍率調整

    private void Awake()
    {
        shadowIdlePosY = hoppingIdleTransform.position.y;
    }

    //影を表示する
    public void ShowShadow()
    {
        gameObject.SetActive(true);
    }

    //足元に影を設置する
    public void FeetShadow(RaycastHit _hit)
    {
        //ShadowScaleAdjustment(_hit);
        transform.position = _hit.point + Vector3.up;
    }

    //影の大きさをホッピングの高さに応じて変える
    private void ShadowScaleAdjustment(RaycastHit _hit)
    {
        if (shadowIdlePosY <= 0) return;

        float _distanceFromGround = hoppingIdleTransform.position.y - _hit.point.y;
        float _heightRatio = _distanceFromGround / shadowIdlePosY;
        shadowScale = 1- (_heightRatio * shadowMagAdjustment);
        shadowScale = Mathf.Clamp01(shadowScale);

        transform.localScale = Vector3.one * shadowScale;
    }

    public void HideShadow()
    {
        gameObject.SetActive(false);
    }
}
