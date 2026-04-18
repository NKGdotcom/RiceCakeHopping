using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーズ画面からゲーム画面に戻るテキストボタン
/// </summary>
public class BackToGameTextController : BaseButton
{
    [Header("アニメーション")]
    [Tooltip("テキストのアニメーション")]
    [SerializeField] private TextAnimation textAnimation;
    [Tooltip("ポーズボタンのアニメーション")]
    [SerializeField] private PauseAnimation pauseAnimation;

    void Awake()
    {
        if (textAnimation == null) { Debug.LogError("textAnimationが参照されていません。"); return; }
        if (pauseAnimation == null) { Debug.LogError("pauseAnimationが参照されていません。"); return;}
    }

    /// <summary>
    /// ボタンをマウスの上に置いたら色を変える
    /// </summary>
    public override void ButtonEnter()
    {
        textAnimation.RedChangeColor();
    }

    /// <summary>
    /// ボタンをマウスから抜けたら色を変える
    /// </summary>
    public override void ButtonExit()
    {
        textAnimation.RedChangeDefaultColor();
    }

    /// <summary>
    /// テキストクリックしたらゲーム画面に戻る
    /// </summary>
    public override void ButtonClick()
    {
        SoundManager.Instance.PlaySE(SESource.BACK_BUTTON);
     
        //ゲーム実行の状態にする
        StageStateController.Instance.ChangeState(StageState.GAME_PLAY);

        //ポーズ画面を抜け、ゲームを再開
        Time.timeScale = 1;
        pauseAnimation.PauseClose();
    }
}
