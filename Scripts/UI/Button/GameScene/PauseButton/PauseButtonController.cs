using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーズボタンの処理をここにまとめる
/// </summary>
public class PauseButtonController : BaseButton
{
    [Header("コンポーネント参照")]
    [Tooltip("ポーズボタン自体のアニメーション")]
    [SerializeField] private PauseButtonAnimation pauseButtonAnimation;
    [Tooltip("ポーズ画面のアニメーション")]
    [SerializeField] private PauseAnimation pauseAnimation;

    // Start is called before the first frame update
    void Awake()
    {
        if(pauseButtonAnimation == null) { Debug.LogError("pauseButtonAnimationが参照されていません"); return; }
        if (pauseAnimation == null) { Debug.LogError("pauseAnimationが参照されていません"); return; }
    }

    /// <summary>
    /// ポーズボタンの上に入ったら色を変える
    /// </summary>
    public override void ButtonEnter()
    {
        pauseButtonAnimation.PauseButtonEnter();
    }

    /// <summary>
    /// ポーズボタンから離れたら色を元に戻す
    /// </summary>
    public override void ButtonExit()
    {
        pauseButtonAnimation.PauseButtonExit();
    }

    /// <summary>
    /// クリックしたらポーズ画面を開く
    /// </summary>
    public override void ButtonClick()
    {
        SoundManager.Instance.PlaySE(SESource.BUTTON);

        //ポーズ状態に
        StageStateController.Instance.ChangeState(StageState.GAME_PAUSE);

        //動かないようにTimeScaleを0に
        Time.timeScale = 0;

        //ポーズ画面を開く
        pauseAnimation.PauseOpen();
    }
}
