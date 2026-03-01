using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルのステージ選択のテキストボタン
/// </summary>
public class StageSelectTextController : BaseButton
{
    [SerializeField] private TextAnimation textAnimation;
    [SerializeField] private TitleAnimation titleAnimation;
    [SerializeField] private PageController stageSelectPageController;

    void Awake()
    {
        if(textAnimation == null) { Debug.LogError("textAnimationが参照されていません。");  return; }
        if(titleAnimation == null) { Debug.LogError("titleAnimationが参照されていません"); return; }
        if (stageSelectPageController == null) { Debug.LogError("stageSelectPageControllerが参照されていません"); return; }
    }

    public override void ButtonEnter()
    {
        textAnimation.RedChangeColor();
    }

    public override void ButtonExit()
    {
        textAnimation.RedChangeDefaultColor();
    }

    public override void ButtonClick()
    {
        OpenStageSelectPage();
    }

    //ステージセレクトのページを開く
    private void OpenStageSelectPage()
    {
        SoundManager.Instance.PlaySE(SESource.button);
        titleAnimation.MovetoStageSelectPage();
        stageSelectPageController.SetPage();
    }
}
