using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルに戻るテキストボタン
/// </summary>
public class BackToTitleTextController : BaseButton
{
    [SerializeField] private TextAnimation textAnimation;
    [SerializeField] private FadeOutController fadeOutController;
    [SerializeField] private TransitionScene transitionScene;

    void Awake()
    {
        if (textAnimation == null) { Debug.LogError("textAnimationが参照されていません。"); return; }
        if (fadeOutController == null) { Debug.LogError("fadeOutControllerが参照されていません。"); return; }
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
        BackTitle().Forget();
    }
    private async UniTaskVoid BackTitle()
    {
        Time.timeScale = 1.0f;
        var _token = this.GetCancellationTokenOnDestroy();
        await fadeOutController.WaitFadeOutAsync(_token);
        transitionScene.ToTitle();
    }
}
