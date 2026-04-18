using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// リザルト画面で同じステージをリトライするボタン
/// </summary>
public class RetryTextController : BaseButton
{
    [Header("コンポーネント参照")]
    [Tooltip("テキストの色を変えるアニメーション")]
    [SerializeField] private TextAnimation textAnimation;
    [Tooltip("画面が変わる際のフェードアウト")]
    [SerializeField] private FadeOutController fadeOutController;
    [Tooltip("シーンを切り替える")]
    [SerializeField] private TransitionScene transitionScene;

    void Awake()
    {
        if (textAnimation == null) { Debug.LogError("textAnimationが参照されていません。"); return; }
        if(fadeOutController == null) { Debug.LogError("fadeOutControllerが参照されていません"); return; }
        if (transitionScene == null) { Debug.LogError("transitionSceneが参照されていません"); return; }
    }

    /// <summary>
    /// マウスをボタンの上に置いたら色を変える
    /// </summary>
    public override void ButtonEnter()
    {
        textAnimation.RedChangeColor();
    }

    /// <summary>
    /// マウスをボタンから離れたら色を元に戻す
    /// </summary>
    public override void ButtonExit()
    {
        textAnimation.RedChangeDefaultColor();
    }

    /// <summary>
    /// ボタンクリックしたら同じステージをもう一度プレイ
    /// </summary>
    public override void ButtonClick()
    {
        SoundManager.Instance.PlaySE(SESource.BUTTON);
        GameRetry().Forget();
    }

    /// <summary>
    /// フェードアウト後ゲームリトライ
    /// </summary>
    /// <returns></returns>
    public async UniTaskVoid GameRetry()
    {
        var _token = this.GetCancellationTokenOnDestroy();

        //フェードアウトの処理が終わるまで待機
        await fadeOutController.WaitFadeOutAsync(_token);

        //ゲームをリトライ
        transitionScene.ToRetryStage();
    }
}
