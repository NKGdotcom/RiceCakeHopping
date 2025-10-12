using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageSelectPage : BaseUIPage
{
    [Header("ステージ選択のテキスト")]
    [SerializeField] private TextMeshProUGUI[] stageText;
    public static StageSelectPage Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
    protected override void Start()
    {
        base.Start();
    }
    public new void OpenPage()
    {
        TitleAnimationState.Instance.MoveToStageSelectPage();
        base.OpenPage();
    }
    public new void NextPage()
    {
        base.NextPage();
    }

    public new void BackPage()
    {
        base.BackPage();
    }
    public new void ClosePage()
    {
        TitleAnimationState.Instance.MoveToTitleNext();
        base.ClosePage();
    }
    public void GoToStage(TextMeshProUGUI _text)
    {
        string _stageName = _text.gameObject.name;
        TitleAnimationState.Instance.GameStart(_stageName);

        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
    }
}
