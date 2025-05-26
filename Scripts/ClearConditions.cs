using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ClearCondition", menuName = "ScriptableObjects/ClearCondition")]
public class ClearConditions : ScriptableObject
{
    [System.Serializable]
    public class ClearCondition //�N���A���@
    {
        public enum Conditions//�V�X�e�[�W�͂�������ǉ����Ă�������
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
        [Header("�N���A�����ꗗ")]
        public Conditions clearConditionType;
        [Header("�N���A�������擾���邽�߂�TagName")]
        public string needTagName;
        [Header("�N���A�������������t")]
        public string clearConditionTextString;
        [Header("�N���A�����ɕK�v�Ȗ݂̑傫��")]
        public float needRiceCakeSize;
        [Header("�X�e�[�W�ԍ�")]
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
