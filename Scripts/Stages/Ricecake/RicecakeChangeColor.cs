using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 触れたフレーバーに合わせて、見た目とエフェクトを変更するクラス
/// </summary>
public class RicecakeChangeColor : MonoBehaviour
{
    [Header("データ参照")]
    [Tooltip("味ごとのマテリアルやエフェクトが登録されたもの")]
    [SerializeField] private RicecakeDataList ricecakeDataList;

    [Header("コンポーネント参照")]
    [Tooltip("ヒット時のエフェクトの管理")]
    [SerializeField] private RicecakeHitEffect ricecakeHitEffect;

    private Renderer ricecakeRenderer;

    private void Awake()
    {
        ricecakeRenderer = GetComponent<Renderer>();

        if (ricecakeDataList == null) { Debug.LogError("ricecakeDataListが参照されていません。"); return; }
        if (ricecakeHitEffect == null) { Debug.LogError("ricecakeHitEffectが参照されていません"); return; }
    }

    /// <summary>
    /// 指定された味付けに他わせて、お餅のマテリアルとエフェクトを差し替える
    /// </summary>
    /// <param name="_ricecakeType"></param>
    public void ChangeRicecakeColor(RicecakeType _ricecakeType)
    {
        foreach(var _data in ricecakeDataList.DataList)
        {
            if(_data.RicecakeType == _ricecakeType)
            {
                //餅の色を変更
                ricecakeRenderer.material = _data.RicecakeMaterial;

                //叩かれた・切られた時のエフェクトを味に合わせたものに変更
                ricecakeHitEffect.SetEffectPrefab(_data.HitEffectPrefab);
                break;
            }
        }
    }
}
