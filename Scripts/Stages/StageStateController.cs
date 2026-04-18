using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ステージ内の進行状況
/// </summary>
public enum StageState 
{
    GAME_READY, //ゲーム開始前(準備中、カウントダウン)
    GAME_PLAY, //ゲームプレイ中
    GAME_PAUSE, //ポーズ(一時停止)中
    GAME_END //ゲーム終了(リザルト中)
}

/// <summary>
/// ステージの状況を管理するクラス
/// </summary>
public class StageStateController : MonoBehaviour
{
    public static StageStateController Instance;

    [Header("現在のステージ状況(デバッグ確認用")]
    [Tooltip("実行中の状態確認用")]
    [SerializeField] StageState nowState = StageState.GAME_READY;
    public StageState NowState => nowState;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    /// <summary>
    /// ステージの状況を新しいものに変更する
    /// </summary>
    /// <param name="_stageState"></param>
    public void ChangeState(StageState _stageState)
    {
        nowState = _stageState;
    }

    /// <summary>
    /// 準備中かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsReady() => nowState == StageState.GAME_READY;

    /// <summary>
    /// プレイ中かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying() => nowState == StageState.GAME_PLAY;

    /// <summary>
    /// ポーズ中かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsPaused() => nowState == StageState.GAME_PAUSE;

    /// <summary>
    /// ゲーム終了したかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsEnd() => nowState == StageState.GAME_END;
}
