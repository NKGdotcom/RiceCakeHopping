using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// タイトルのアニメーションをまとめる
/// </summary>
public class TitleAnimation : MonoBehaviour
{
    [Header("アニメーション")]
    [Tooltip("タイトルのアニメーション")]
    [SerializeField] private Animator titleAnimator;

    [Header("コンポーネント参照")]
    [Tooltip("フェードアウトするアニメーション")]
    [SerializeField] private FadeOutController fadeout;
    [Tooltip("シーン遷移する")]
    [SerializeField] private TransitionScene transitionScene;

    //アニメーショントリガー
    //タイトルからボタンを押す
    private const string STR_TAP_ANY_KEY = "TapAnyKey";
    //ステージ選択に移る
    private const string STR_GO_TO_STAGESELECT = "GoToStageSelect";
    //遊び方に移る
    private const string STR_GO_TO_HOWTOPLAY = "GoToHowToPlay";
    //スタート

    // Start is called before the first frame update
    void Awake()
    {
        if (titleAnimator == null) { Debug.LogError("titleAnimatorが参照されていません"); return; }
        if(fadeout == null) { Debug.LogError("fadeoutが参照されていません"); return; }
        if (transitionScene == null) { Debug.LogError("transitionScene"); return; }
    }

    /// <summary>
    /// タイトルに戻るアニメーション
    /// </summary>
    public void MovetoTitleNext()
    {
        titleAnimator.SetTrigger(STR_TAP_ANY_KEY);
        titleAnimator.SetBool(STR_GO_TO_STAGESELECT, false);
        titleAnimator.SetBool(STR_GO_TO_HOWTOPLAY, false);
    }

    /// <summary>
    /// ステージ選択に移るアニメーション
    /// </summary>
    public void MovetoStageSelectPage()
    {
        titleAnimator.SetBool(STR_GO_TO_STAGESELECT, true);
    }

    /// <summary>
    /// 遊び方のページに進むアニメーション
    /// </summary>
    public void MovetoHowtoPlayPage()
    {
        titleAnimator.SetBool(STR_GO_TO_HOWTOPLAY, true);
    }

    /// <summary>
    /// ゲームを開始する
    /// </summary>
    /// <param name="_stageName"></param>
    /// <returns></returns>
    public async UniTaskVoid GameStart(string _stageName)
    {
        var _token = this.GetCancellationTokenOnDestroy();

        //フェードアウトが終わるまで待機
        await fadeout.WaitFadeOutAsync(_token);

        //ステージ遷移
        transitionScene.ToSelectStage(_stageName);
    }
}
