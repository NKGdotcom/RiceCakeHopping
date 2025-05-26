using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ClearCondition", menuName = "ScriptableObjects/ClearCondition")]
public class ClearConditions : ScriptableObject
{
    [System.Serializable]
    public class ClearCondition //クリア方法
    {
        public enum Conditions//新ステージはここから追加してください
        {
            Stage1,
            Stage2,
            Stage3,
            Stage4,
            Stage5,
            Stage6,
            Stage7,
            Stage8,
            Stage9,
            Stage10,
            Stage11,
            Stage12,
            Stage13,
            Stage14,
            Stage15,
            Stage16,
            Stage17,
            Stage18,
            Stage19,
            Stage20,
            Stage21,
            Stage22,
            Stage23,
            Stage24,
            Stage25,
            Stage26,
            Stage27,
            Stage28,
            Stage29,
            Stage30,
        }
        [Header("クリア条件一覧")]
        public Conditions clearConditionType;
        [Header("クリア条件を取得するためのTagName")]
        public string needTagName;
        [Header("クリア条件を示す言葉")]
        public string clearConditionTextString;
        [Header("クリア条件に必要な餅の大きさ")]
        public float needRiceCakeSize;
        [Header("ステージ番号")]
        public int stageNum;

        public ClearCondition(Conditions clearConditionType, string needTagName, string clearConditionTextString, float needRiceCakeSize, int stageNum)
        {
            this.clearConditionType = clearConditionType;
            this.needTagName = needTagName;
            this.clearConditionTextString = clearConditionTextString;
            this.needRiceCakeSize = needRiceCakeSize;
            this.stageNum = stageNum;
        }
    }
    public List<ClearCondition> clearConditionList = new List<ClearCondition>();

    public ClearCondition GetClearCondition(ClearCondition.Conditions conditionType)
    {
        foreach (ClearCondition condition in clearConditionList)
        {
            if(condition.clearConditionType == conditionType)
            {
                return new ClearCondition(condition.clearConditionType, condition.needTagName, condition.clearConditionTextString, condition.needRiceCakeSize, condition.stageNum);
            }
        }
        return null;
    }
}
