using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルの遊び方のページに進むボタン
/// </summary>
public class HowToPlayTextController : BaseButton
{
    [Header("コンポーネント参照")]
    [Tooltip("テキストの色を変えるアニメーション")]
    [SerializeField] private TextAnimation textAnimation;
    [Tooltip("タイトルでのアニメーション")]
    [SerializeField] private TitleAnimation titleAnimation;
    [Tooltip("遊び方のページ")]
    [SerializeField] private PageController howToPlayPageController;

    private void Awake()
    {
        if (textAnimation == null) { Debug.LogError("textAnimationが参照されていません。"); return; }
        if (titleAnimation == null) { Debug.LogError("titleAnimationが参照されていません"); return; }
        if (howToPlayPageController == null) { Debug.LogError("howToPlayPageControllerが参照されていません"); return; }
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

        //遊び方のページを開く
        titleAnimation.MovetoHowtoPlayPage();

        howToPlayPageController.SetPage();
    }
}
