using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの制限時間を管理するクラス
/// </summary>
public class TimeController : MonoBehaviour
{
    [Header("UI・演出の参照")]
    [Tooltip("制限時間を画面に表示するUIクラス")]
    [SerializeField] private TimeView timeView;
    [Tooltip("時間切れ時の失敗演出を呼び出すためのクラス")]
    [SerializeField] private ResultView resultView;

    [Header("時間設定")]
    [Tooltip("ゲーム開始前の待機秒数")]
    private float gameStartDelay = 2;
    
    //現在の残り時間
    private float timer;
    //時間切れ処理が何度も呼ばれるのを防ぐ
    private bool isTimeUp = false;

    private void Awake()
    {
        if(timeView == null) { Debug.LogError("timeViewが参照されていません"); return; }
        if(resultView == null) { Debug.LogError("resultViewが参照されていません"); return; }

        WaitStart().Forget();
    }

    void Update()
    {
        //ゲームプレイ中のみ
        if (StageStateController.Instance.IsPlaying())
        {
            if (timer > 0)
            {
                //時間を減らしてUIを更新
                timer -= Time.deltaTime;
                timeView.UpdateTMP(timer);
            }

            else if (timer <= 0 && !isTimeUp)
            {
                //タイプアップ処理
                isTimeUp = true;
                timer = 0;
                timeView.UpdateTMP(timer);

                StageStateController.Instance.ChangeState(StageState.GAME_END);

                //失敗演出の呼び出し
                resultView.NotEatAnimationAsync().Forget();
            }
        }
    }

    /// <summary>
    /// ゲーム開始時にステージの制限時間を設定
    /// </summary>
    /// <param name="_limitTime"></param>
    public void SetupTime(float _limitTime)
    {
        timer = _limitTime;
    }

    /// <summary>
    /// ゲーム開始前に一定時間待機し、その後プレイ状態へ移行
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid WaitStart()
    {
        //どんな味付けが欲しいかの準備段階
        StageStateController.Instance.ChangeState(StageState.GAME_READY);
        
        var _token = this.GetCancellationTokenOnDestroy();
        
        //ゲームが始まるまで待機
        await UniTask.WaitForSeconds(gameStartDelay, cancellationToken: _token);
        
        StageStateController.Instance.ChangeState(StageState.GAME_PLAY);
    }
}
