using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �N���A������ClearConditions����擾���ݒ�
/// </summary>
public class SetClearConditions : MonoBehaviour
{
    [SerializeField] private ClearConditions clearConditions;
    [SerializeField] private TextMeshProUGUI conditoinText;
    private int stageIndexNum; //���X�g����擾����ۂ̃X�e�[�W�ԍ�(n-1����)
    private string stageNum;
    private string stringSeasoningRiceCake;
    private string stringRiceCakeSize;
    //�݂̑傫��
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
    /// ScriptableObject�Őݒ肵���N���A�������擾����
    /// </summary>
    public void SetClearCondition()
    {
        ClearConditions.ClearCondition clearConditionData = clearConditions.GetClearCondition(stageIndexNum);

        if (clearConditionData != null)
        {
            switch(clearConditionData.conditionsRiceCakeName)//�����Őݒ肵���݂̎��
            {
                case (ClearConditions.ClearCondition.ConditionsRiceCakeName.Normal):
                    RiceCakeTag = "Normal";
                    stringSeasoningRiceCake = "���t���Ȃ�";
                    break;
                case (ClearConditions.ClearCondition.ConditionsRiceCakeName.Kinako):
                    RiceCakeTag = "KinakoRiceCake";
                    stringSeasoningRiceCake = "���ȕ���";
                    break;
                case (ClearConditions.ClearCondition.ConditionsRiceCakeName.SoySource):
                    RiceCakeTag = "SoySourceRiceCake";
                    stringSeasoningRiceCake = "�ݖ���";
                    break;
            }
            switch (clearConditionData.conditionsSize)//�����Őݒ肵���݂̑傫��
            {
                case (ClearConditions.ClearCondition.ConditionsSize.Normal):
                    RiceCakeSize = normalSize;
                    stringRiceCakeSize = "��";
                    break;
                case (ClearConditions.ClearCondition.ConditionsSize.Big):
                    RiceCakeSize = bigSize;
                    stringRiceCakeSize = "�傫����";
                    break;
                case (ClearConditions.ClearCondition.ConditionsSize.VeryBig):
                    RiceCakeSize = veryBigSize;
                    stringRiceCakeSize = "�ƂĂ��傫����";
                    break;
                case (ClearConditions.ClearCondition.ConditionsSize.Small):
                    RiceCakeSize = smallSize;
                    stringRiceCakeSize = "��������";
                    break;
            }
            ConditionText = $"{stringSeasoningRiceCake}��{stringRiceCakeSize}�H������";
            conditoinText.text = ConditionText;
        }
    }
}
