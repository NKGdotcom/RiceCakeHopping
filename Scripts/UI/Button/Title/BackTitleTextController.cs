using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージセレクトや、遊び方からタイトルに戻る
/// </summary>
public class BackTitleTextController : BaseButton
{
    [Header("コンポーネント参照")]
    [Tooltip("テキストの色を変える")]
    [SerializeField] private TextAnimation textAnimation;
    [Tooltip("タイトル画面のアニメーション")]
    [SerializeField] private TitleAnimation titleAnimation;
    public event Action OnClicked;

    void Awake()
    {
        if (textAnimation == null) { Debug.LogError("textAnimationが参照されていません。"); return; }
        if(titleAnimation == null) { Debug.LogError("titleAnimationが参照されていません"); return; }
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
    /// ボタンの上でマウスをクリックしたらタイトル画面に戻る
    /// </summary>
    public override void ButtonClick()
    {
        SoundManager.Instance.PlaySE(SESource.BACK_BUTTON);

        //クリックしたことを知らせる
        OnClicked?.Invoke();

        //タイトルに戻る
        titleAnimation.MovetoTitleNext();
    }
}
