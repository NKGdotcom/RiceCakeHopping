using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルトをしっかりと見せるために非表示にする
/// </summary>
public class HideStageObject : MonoBehaviour
{
    [SerializeField] private GameObject[] stageObjects;
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject hopping;
    [SerializeField] private GameObject feetShadow;

    //ステージ上のすべてのオブジェクトを非表示
    public void AllHideStageObj()
    {
        if (stageObjects == null) return;

        HideBaseStageObj();
        table.SetActive(false);
    }

    //テーブル以外をすべて非表示
    //食えないじゃないかの時、机を使ったアニメーションをするため
    public void FailedResultHideStageObj()
    {
        if(stageObjects == null) return;

        HideBaseStageObj();
    }

    //どの状態でも消すオブジェクト
    private void HideBaseStageObj()
    {
        foreach(var _stageObj in stageObjects)
        {
            _stageObj.SetActive(false);
        }
        hopping.SetActive(false);
        feetShadow.SetActive(false);
    }
}
