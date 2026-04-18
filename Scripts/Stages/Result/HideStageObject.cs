using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルト画面を移行する際、背景となるステージ上のオブジェクトを非表示にする
/// </summary>
public class HideStageObject : MonoBehaviour
{
    [Header("常に非表示にするオブジェクト")]
    [Tooltip("ステージ上のギミックや障害物")]
    [SerializeField] private GameObject[] stageObjects;
    [Tooltip("ホッピング本体")]
    [SerializeField] private GameObject hopping;
    [Tooltip("ホッピングの足元の影")]
    [SerializeField] private GameObject feetShadow;

    [Header("状況によって残すオブジェクト")]
    [Tooltip("食えないじゃないかの失敗演出の時は非表示にしない")]
    [SerializeField] private GameObject table;

    private void Awake()
    {
        if(hopping == null) { Debug.LogError("hoppingが参照されていません"); return; }
        if (feetShadow == null) { Debug.LogError("feetShadowが参照されていません"); return; }
        if(table == null) { Debug.LogError("tableが参照されていません"); return; }
    }

    /// <summary>
    /// 通常の演出としてステージ上のすべてのオブジェクトを非表示
    /// </summary>
    public void AllHideStageObj()
    {
        //基本消すオブジェクトたち
        HideBaseStageObj();

        table.SetActive(false);
    }

    /// <summary>
    /// 失敗リザルトであれば、テーブル以外をすべて非表示にする
    /// </summary>
    public void FailedResultHideStageObj()
    {
        //テーブル以外のオブジェクトをすべて非表示
        HideBaseStageObj();
    }

    /// <summary>
    /// どの状態でも必ず消すオブジェクト
    /// </summary>
    private void HideBaseStageObj()
    {
        if(stageObjects != null)
        {
            foreach (var _stageObj in stageObjects)
            {
                //ステージ上のギミックを非表示
                _stageObj.SetActive(false);
            }
        }

        hopping.SetActive(false);
        feetShadow.SetActive(false);
    }
}
