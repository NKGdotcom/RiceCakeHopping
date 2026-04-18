using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージセレクトのテキストに付ける
/// </summary>
public class StageTextController : BaseButton
{
    [Header("コンポーネントの参照")]
    [Tooltip("テキストの色を変える")]
    [SerializeField] private TextAnimation textAnimation;
    [Tooltip("タイトル画面のアニメーション")]
    [SerializeField] private TitleAnimation titleAnimation;
    [Header("ステージ情報")]
    [Tooltip("遊ぶステージ名")]
    [SerializeField] private string stageName = "Stage";

    void Awake()
    {
        if (textAnimation == null) { Debug.LogError("textAnimationが参照されていません。"); return; }
        if (titleAnimation == null) { Debug.LogError("titleAnimationは参照されていません。"); return; }
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
    /// ボタンの上でマウスをクリックしたら設定したステージ名を持つシーンに移動
    /// </summary>
    public override void ButtonClick()
    {
        SoundManager.Instance.PlaySE(SESource.BUTTON);
        titleAnimation.GameStart(stageName).Forget();
    }
}
