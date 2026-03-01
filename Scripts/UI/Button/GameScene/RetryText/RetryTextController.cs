using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームをリトライするテキストボタン
/// </summary>
public class RetryTextController : BaseButton
{
    [SerializeField] private TextAnimation textAnimation;
    [SerializeField] private FadeOutController fadeOutController;
    [SerializeField] private TransitionScene transitionScene;

    // Start is called before the first frame update
    void Awake()
    {
        if (textAnimation == null) { Debug.LogError("textAnimationが参照されていません。"); return; }
        if (transitionScene == null) { Debug.LogError("transitionSceneが参照されていません"); return; }
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
        GameRetry().Forget();
    }

    public async UniTaskVoid GameRetry()
    {
        var _token = this.GetCancellationTokenOnDestroy();
        await fadeOutController.WaitFadeOutAsync(_token);
        transitionScene.ToRetryStage();
    }
}
