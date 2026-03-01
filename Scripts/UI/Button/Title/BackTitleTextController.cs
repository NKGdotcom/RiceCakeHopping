using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージセレクトや、遊び方のところのタイトルに戻る
/// </summary>
public class BackTitleTextController : BaseButton
{
    [SerializeField] private TextAnimation textAnimation;
    [SerializeField] private TitleAnimation titleAnimation;
    public event Action IsClicked;

    void Awake()
    {
        if (textAnimation == null) { Debug.LogError("textAnimationが参照されていません。"); return; }
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
        SoundManager.Instance.PlaySE(SESource.backButton);
        IsClicked?.Invoke();
        titleAnimation.MovetoTitleNext();
    }
}
