using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームに戻るテキストボタン
/// </summary>
public class BackToGameTextController : BaseButton
{
    [SerializeField] private TextAnimation textAnimation;
    [SerializeField] private PauseAnimation pauseAnimation;

    void Awake()
    {
        if (textAnimation == null) { Debug.LogError("textAnimationが参照されていません。"); return; }
        if (pauseAnimation == null) { Debug.LogError("pauseAnimationが参照されていません。"); return;}
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
        StageStateController.Instance.ChangeState(StageState.GAME_PLAY);
        Time.timeScale = 1;
        pauseAnimation.PauseClose();
    }
}
