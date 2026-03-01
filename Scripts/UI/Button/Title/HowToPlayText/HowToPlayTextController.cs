using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルの遊び方のテキストボタン
/// </summary>
public class HowToPlayTextController : BaseButton
{
    [SerializeField] private TextAnimation textAnimation;
    [SerializeField] private TitleAnimation titleAnimation;
    [SerializeField] private PageController howToPlayPageController;

    private void Awake()
    {
        if (textAnimation == null) { Debug.LogError("textAnimationが参照されていません。"); return; }
        if (titleAnimation == null) { Debug.LogError("titleAnimationが参照されていません"); return; }
        if (howToPlayPageController == null) { Debug.LogError("howToPlayPageControllerが参照されていません"); return; }
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
        SoundManager.Instance.PlaySE(SESource.button);
        OpenHowToPlayPage();
    }

    //遊び方のページを開く
    private void OpenHowToPlayPage()
    {
        titleAnimation.MovetoHowtoPlayPage();
        howToPlayPageController.SetPage();
    }
}
