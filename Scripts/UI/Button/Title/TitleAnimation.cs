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
    [SerializeField] private Animator titleAnimator;
    [SerializeField] private FadeOutController fadeout;
    [SerializeField] private TransitionScene transitionScene;

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
        if(transitionScene == null) { Debug.LogError("transitionScene"); return; }
    }

    //タイトルに戻る
    public void MovetoTitleNext()
    {
        titleAnimator.SetTrigger(STR_TAP_ANY_KEY);
        titleAnimator.SetBool(STR_GO_TO_STAGESELECT, false);
        titleAnimator.SetBool(STR_GO_TO_HOWTOPLAY, false);
    }

    //ステージ選択に移る
    public void MovetoStageSelectPage()
    {
        titleAnimator.SetBool(STR_GO_TO_STAGESELECT, true);
    }

    //遊び方に進む
    public void MovetoHowtoPlayPage()
    {
        titleAnimator.SetBool(STR_GO_TO_HOWTOPLAY, true);
    }

    //ゲームスタート
    public async UniTaskVoid GameStart(string _stageName)
    {
        var _token = this.GetCancellationTokenOnDestroy();

        await fadeout.WaitFadeOutAsync(_token);
        transitionScene.ToSelectStage(_stageName);
    }
}
