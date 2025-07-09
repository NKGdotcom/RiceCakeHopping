using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ClearCondition", menuName = "ScriptableObjects/ClearCondition")]
public class ClearConditions : ScriptableObject
{
    [System.Serializable]
    public class ClearCondition //�N���A���@
    {
        public enum Conditions //�V�X�e�[�W�͂�������ǉ����Ă�������
        {
            Stage1,Stage2,Stage3,Stage4,Stage5,Stage6,Stage7,Stage8,Stage9,Stage10,Stage11,Stage12,Stage13,Stage14,Stage15,
            Stage16,Stage17,Stage18,Stage19,Stage20,Stage21,Stage22,Stage23,Stage24,Stage25,Stage26,Stage27,Stage28,Stage29,Stage30,
        }
        public enum ConditionsRiceCakeName //�݂̎��
        {
            Normal,SoySource,Kinako
        }
        /*public enum ConditionsString //�N���A�����̕���
        {
            VeryDelicious,SoDelicious,TooMuch,NotEnough,SomethingGotIt,NotEat
        }*/
        public enum ConditionsSize //�N���A�����̖݂̃T�C�Y
        {
            VeryBig,Big,Normal,Small
        }
        [Header("�N���A�����ꗗ")]
        public Conditions clearConditionType;
        [Header("�N���A�������擾���邽�߂�TagName")]
        public ConditionsRiceCakeName conditionsRiceCakeName;
        //[Header("�N���A�������������t�̐ݒ�")]
       // public ConditionsString conditionsString;
        [Header("�N���A�����ɕK�v�Ȗ݂̑傫��")]
        public ConditionsSize conditionsSize;
        [Header("�X�e�[�W�ԍ�")]
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
