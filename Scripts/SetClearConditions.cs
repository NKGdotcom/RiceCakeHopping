using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// クリア条件をClearConditionsから取得し設定
/// </summary>
public class SetClearConditions : MonoBehaviour
{
    [SerializeField] private ClearConditions clearConditions;
    [SerializeField] private TextMeshProUGUI conditoinText;
    private int stageIndexNum; //リストから取得する際のステージ番号(n-1する)
    private string stageNum;
    private string stringSeasoningRiceCake;
    private string stringRiceCakeSize;
    //餅の大きさ
    private const float veryBigSize = 3f;
    private const float bigSize = 2f;
    private const float normalSize = 1f;
    private const float smallSize = 0.5f;

    public string RiceCakeTag {  get; private set; }
    public string ConditionText {  get; private set; }
    public float RiceCakeSize {  get; private set; }
    public int StageIndexNum { get; private set; }


    private void Awake()
    {
        stageNum = SceneManager.GetActiveScene().name;
        stageNum = stageNum.Replace("Stage", "");
        stageIndexNum = int.Parse(stageNum);
        StageIndexNum = stageIndexNum;

        stageIndexNum--;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetClearCondition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// ScriptableObjectで設定したクリア条件を取得する
    /// </summary>
    public void SetClearCondition()
    {
        ClearConditions.ClearCondition clearConditionData = clearConditions.GetClearCondition(stageIndexNum);

        if (clearConditionData != null)
        {
            switch(clearConditionData.conditionsRiceCakeName)//条件で設定した餅の種類
            {
                case (ClearConditions.ClearCondition.ConditionsRiceCakeName.Normal):
                    RiceCakeTag = "Normal";
                    stringSeasoningRiceCake = "味付けなし";
                    break;
                case (ClearConditions.ClearCondition.ConditionsRiceCakeName.Kinako):
                    RiceCakeTag = "KinakoRiceCake";
                    stringSeasoningRiceCake = "黄な粉味";
                    break;
                case (ClearConditions.ClearCondition.ConditionsRiceCakeName.SoySource):
                    RiceCakeTag = "SoySourceRiceCake";
                    stringSeasoningRiceCake = "醤油味";
                    break;
            }
            switch (clearConditionData.conditionsSize)//条件で設定した餅の大きさ
            {
                case (ClearConditions.ClearCondition.ConditionsSize.Normal):
                    RiceCakeSize = normalSize;
                    stringRiceCakeSize = "餅";
                    break;
                case (ClearConditions.ClearCondition.ConditionsSize.Big):
                    RiceCakeSize = bigSize;
                    stringRiceCakeSize = "大きい餅";
                    break;
                case (ClearConditions.ClearCondition.ConditionsSize.VeryBig):
                    RiceCakeSize = veryBigSize;
                    stringRiceCakeSize = "とても大きい餅";
                    break;
                case (ClearConditions.ClearCondition.ConditionsSize.Small):
                    RiceCakeSize = smallSize;
                    stringRiceCakeSize = "小さい餅";
                    break;
            }
            ConditionText = $"{stringSeasoningRiceCake}の{stringRiceCakeSize}食いたい";
            conditoinText.text = ConditionText;
        }
    }
}
