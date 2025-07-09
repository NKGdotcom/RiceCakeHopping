using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageStageSelect : UIPageMove
{
    [Header("�X�e�[�W�I���̃e�L�X�g")]
    [SerializeField] private TextMeshProUGUI[] stageText;

    private static readonly Color32 highlightColor = new Color32(255, 130, 130, 255);//�e�L�X�g�̏�ɒu�����班���Ԃ��ۂ��F��
    private static readonly Color32 textDefaultColor = new Color32(0, 0, 0, 255);

    private void Awake()
    {
        movePage = (eventData) => { TitleAnimationState.Instance.MoveToStageSelectPage(); };
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        for(int i =0;  i < stageText.Length; i++)
        {
            int _currentIndex = i;

            SetUpTextEvent(stageText[_currentIndex].gameObject,
                            (eventData) => { StartColorChange(stageText[_currentIndex], highlightColor); },
                            (eventData) => { StartColorChange(stageText[_currentIndex], textDefaultColor); },
                            (eventData) => { GoToStage(stageText[_currentIndex]); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// �X�e�[�W�Ɉړ�
    /// </summary>
    /// <param name="_text">�e�L�X�g�̖��O���擾</param>
    private void GoToStage(TextMeshProUGUI _text)
    {
        string _stageName = _text.gameObject.name;
        TitleAnimationState.Instance.GameStart(_stageName);

        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
    }
}
