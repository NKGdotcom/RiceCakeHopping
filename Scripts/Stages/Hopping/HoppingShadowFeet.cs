using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングの足元に影を設置し、ジャンプの高さに応じて影の大きさを変える
/// FeetShadowとShadowScaleAdjustomentで迷っている
/// /// </summary>
public class HoppingShadowFeet : MonoBehaviour
{
    [Header("参照")]
    [Tooltip("高さ計算の機銃となるホッピング本体のTransform")]
    [SerializeField] private Transform hoppingIdleTransform;

    [Header("調整パラメータ")]
    [Tooltip("高さに応じた影の縮小率")]
    private float shadowMagAdjustment = 0.5f;
    [Tooltip("地面に埋まらない程度の高さに影を設置")]
    [SerializeField] private float groundHeightOffset = 0.1f;

    //基準となる初期の高さ
    private float shadowIdlePosY;


    private void Awake()
    {
        if(hoppingIdleTransform == null) { Debug.LogError("hoppingIdleTransformが参照されていません"); return; }

        shadowIdlePosY = hoppingIdleTransform.position.y;
    }

    /// <summary>
    /// 足元にRayを飛ばし、地面があれば影を設置
    /// </summary>
    public void UpdateShadow()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            ShowShadow();
            FeetShadow(hit);
        }
        else
        {
            HideShadow();
        }
    }

    /// <summary>
    /// 影を表示する
    /// </summary>
    private void ShowShadow() => gameObject.SetActive(true);

    /// <summary>
    /// 影の高さが低かったら非表示
    /// </summary>
    private void HideShadow() => gameObject.SetActive(false);

    /// <summary>
    /// 足元に影を設置する
    /// </summary>
    /// <param name="_hit"></param>
    public void FeetShadow(RaycastHit _hit)
    {
        //ShadowScaleAdjustment(_hit);
        transform.position = _hit.point + Vector3.up * groundHeightOffset;
    }

    /// <summary>
    /// 影の大きさをホッピングの現在位置の高さと地面との距離に応じて変える
    /// </summary>
    /// <param name="_hit"></param>
    private void ShadowScaleAdjustment(RaycastHit _hit)
    {
        //ゼロ割計算を防ぐ
        if (shadowIdlePosY <= 0) return;

        //地面からホッピング本体までの距離を計算
        float _distanceFromGround = hoppingIdleTransform.position.y - _hit.point.y;
        
        //距離の割合を計算し影のスケールを算出
        float _heightRatio = _distanceFromGround / shadowIdlePosY;
        float _shadowScale = 1f - (_heightRatio * shadowMagAdjustment);

        //マイナスや1以上にならないように制限
        _shadowScale = Mathf.Clamp01(_shadowScale);

        //影の大きさを反映
        transform.localScale = Vector3.one * _shadowScale;
    }
    
}
