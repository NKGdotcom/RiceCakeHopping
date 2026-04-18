using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// リザルト画面から次のステージに進む
/// </summary>
public class NextStageTextController : BaseButton
{
    [Header("コンポーネント参照")]
    [Tooltip("テキストの色を変えるアニメーション")]
    [SerializeField] private TextAnimation textAnimation;
    [Tooltip("フェードアウト")]
    [SerializeField] private FadeOutController fadeOutController;
    [Tooltip("シーンの遷移をする")]
    [SerializeField] private TransitionScene transitionScene;

    // Start is called before the first frame update
    void Awake()
    {
        if (textAnimation == null) { Debug.LogError("textAnimationが参照されていません。"); return; }
        if (fadeOutController == null) { Debug.LogError("fadeOutControllerが参照されていません。"); return; }
        if (transitionScene == null) { Debug.LogError("transitionSceneが参照されていません"); return; }
    }

    /// <summary>
    /// ボタンの上にマウスを置いたら色を変える
    /// </summary>
    public override void ButtonEnter()
    {
        textAnimation.RedChangeColor();
    }

    /// <summary>
    /// ボタンの上からマウスを離したら色を元に戻す
    /// </summary>
    public override void ButtonExit()
    {
        textAnimation.RedChangeDefaultColor();
    }

    /// <summary>
    /// ボタンの上でマウスをクリックしたら次のステージに遷移
    /// </summary>
    public override void ButtonClick()
    {
        SoundManager.Instance.PlaySE(SESource.BUTTON);
        NextStage().Forget();
    }

    /// <summary>
    /// 次のステージに進む
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid NextStage()
    {
        var _token = this.GetCancellationTokenOnDestroy();

        //フェードアウトの処理が終わるまで待機
        await fadeOutController.WaitFadeOutAsync(_token);

        //次のステージに移る
        transitionScene.ToNextStage();
    }
}
