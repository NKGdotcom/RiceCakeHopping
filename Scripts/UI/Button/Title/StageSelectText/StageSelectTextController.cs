using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルのステージ選択のテキストボタン
/// </summary>
public class StageSelectTextController : BaseButton
{
    [Header("コンポーネント参照")]
    [Tooltip("テキストの色を変えるアニメーション")]
    [SerializeField] private TextAnimation textAnimation;
    [Tooltip("タイトル画面のアニメーション")]
    [SerializeField] private TitleAnimation titleAnimation;
    [Tooltip("ステージ選択画面のページ管理")]
    [SerializeField] private PageController stageSelectPageController;

    void Awake()
    {
        if(textAnimation == null) { Debug.LogError("textAnimationが参照されていません。");  return; }
        if(titleAnimation == null) { Debug.LogError("titleAnimationが参照されていません"); return; }
        if (stageSelectPageController == null) { Debug.LogError("stageSelectPageControllerが参照されていません"); return; }
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

        //ステージ選択画面に移る
        titleAnimation.MovetoStageSelectPage();
        stageSelectPageController.SetPage();
    }
}
