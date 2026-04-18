using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルトやポーズ画面からタイトルに戻るテキストボタン
/// </summary>
public class BackToTitleTextController : BaseButton
{
    [Header("コンポーネント参照")]
    [Tooltip("テキストの色を変えるアニメーション")]
    [SerializeField] private TextAnimation textAnimation;
    [Tooltip("フェードアウトのアニメーション")]
    [SerializeField] private FadeOutController fadeOutController;
    [Tooltip("シーンの遷移")]
    [SerializeField] private TransitionScene transitionScene;

    void Awake()
    {
        if (textAnimation == null) { Debug.LogError("textAnimationが参照されていません。"); return; }
        if (fadeOutController == null) { Debug.LogError("fadeOutControllerが参照されていません。"); return; }
        if(transitionScene == null) { Debug.LogError("transitionSceneが参照されていません"); return; }
    }

    /// <summary>
    /// ボタンの上にマウスを置いたら赤色を変える
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
    /// ボタンをクリックし、タイトルに戻る
    /// </summary>
    public override void ButtonClick()
    {
        SoundManager.Instance.PlaySE(SESource.BUTTON);
        BackTitle().Forget();
    }

    /// <summary>
    /// タイトルに戻るための遷移
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid BackTitle()
    {
        //ポーズ画面から呼ばれた場合を考慮し、時間の進みを元に戻す
        Time.timeScale = 1.0f;

        var _token = this.GetCancellationTokenOnDestroy();

        //フェードアウト完了を待機
        await fadeOutController.WaitFadeOutAsync(_token);

        //タイトルへ遷移
        transitionScene.ToTitle();
    }
}
