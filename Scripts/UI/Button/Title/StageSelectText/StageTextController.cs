using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージセレクトのテキストに付ける
/// </summary>
public class StageTextController : BaseButton
{
    [SerializeField] private TextAnimation textAnimation;
    [SerializeField] private TitleAnimation titleAnimation;
    [SerializeField] private string stageName = "Stage";

    void Awake()
    {
        if (textAnimation == null) { Debug.LogError("textAnimationが参照されていません。"); return; }
        if (titleAnimation == null) { Debug.LogError("titleAnimationは参照されていません。"); return; }
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
        titleAnimation.GameStart(stageName).Forget();
    }
}
