using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ClearCondition", menuName = "ScriptableObjects/ClearCondition")]
public class ClearConditions : ScriptableObject
{
    [System.Serializable]
    public class ClearCondition //クリア方法
    {
        public enum Conditions //新ステージはここから追加してください
        {
            Stage1,Stage2,Stage3,Stage4,Stage5,Stage6,Stage7,Stage8,Stage9,Stage10,Stage11,Stage12,Stage13,Stage14,Stage15,
            Stage16,Stage17,Stage18,Stage19,Stage20,Stage21,Stage22,Stage23,Stage24,Stage25,Stage26,Stage27,Stage28,Stage29,Stage30,
        }
        public enum ConditionsRiceCakeName //餅の種類
        {
            Normal,SoySource,Kinako
        }
        /*public enum ConditionsString //クリア条件の文字
        {
            VeryDelicious,SoDelicious,TooMuch,NotEnough,SomethingGotIt,NotEat
        }*/
        public enum ConditionsSize //クリア条件の餅のサイズ
        {
            VeryBig,Big,Normal,Small
        }
        [Header("クリア条件一覧")]
        public Conditions clearConditionType;
        [Header("クリア条件を取得するためのTagName")]
        public ConditionsRiceCakeName conditionsRiceCakeName;
        //[Header("クリア条件を示す言葉の設定")]
       // public ConditionsString conditionsString;
        [Header("クリア条件に必要な餅の大きさ")]
        public ConditionsSize conditionsSize;
        [Header("ステージ番号")]
        public int stageNum;

        public ClearCondition(Conditions _clearConditionType, ConditionsRiceCakeName _conditionsRiceCakeNum, /*ConditionsString clearConditionTextString,*/ ConditionsSize _conditionsSize, int _stageNum)
        {
            this.clearConditionType = _clearConditionType;
            this.conditionsRiceCakeName = _conditionsRiceCakeNum;
            //this.conditionsString = clearConditionTextString;
            this.conditionsSize = _conditionsSize;
            this.stageNum = _stageNum;
        }
    }
    public List<ClearCondition> clearConditionList = new List<ClearCondition>();

    public ClearCondition GetClearCondition(int _index)
    {
        if(_index >= 0 &&  _index < clearConditionList.Count)
        {
            return clearConditionList[_index];
        }
        return null;
    }
}
