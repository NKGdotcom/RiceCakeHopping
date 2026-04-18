using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ開始時の初期設定を統括するコントローラー
/// </summary>
public class StageController : MonoBehaviour
{
    [Header("ステージデータ")]
    [Tooltip("このステージの制限時間やクリア条件が定義されたScriptableObject")]
    [SerializeField] private StageData stageData;

    [Header("各システムへの参照")]
    [Tooltip("リザルトの計算を行うシステム")]
    [SerializeField] private ResultCalculation resultCalculation;
    [Tooltip("制限時間のカウントと管理を行うシステム")]
    [SerializeField] private TimeController timeController;
    [Tooltip("ステージのクリア条件を判定・管理するシステム")]
    [SerializeField] private ConditionController conditionController;

    void Awake()
    {
        if (stageData == null) { Debug.LogError("stageDataが参照されていません"); return; }
        if (resultCalculation == null) { Debug.LogError("resultCalculationが参照されていません"); return; }
        if (timeController == null) { Debug.LogError("timeControllerが参照されていません"); return; }
        if (conditionController == null) { Debug.LogError("conditionControllerが参照されていません。"); return; }

        Setup();
    }

    /// <summary>
    /// StageDataから必要な情報を摘出し、初期設定を行う
    /// </summary>
    private void Setup()
    {
        //クリア条件やスコア基準のデータを渡す
        resultCalculation.SetResultCondition(stageData);

        //このステージの制限時間をセット
        timeController.SetupTime(stageData.TimeLimit);

        //ステージの目標をセットする
        conditionController.SetCondition(stageData.StageDescription);
    }
}
