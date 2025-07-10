using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの状態を設定
/// </summary>
public class GameStateMachine : MonoBehaviour
{
    public static GameStateMachine Instance { get; private set; }

    public enum GameState {IntroduceClear,Playing,Pause,Result }

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        SetState(GameState.IntroduceClear);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 状態を更新
    /// </summary>
    /// <param name="_state">変更したい状態</param>
    public void SetState(GameState _state)
    {
        CurrentState = _state;
    }

    /// <summary>
    /// クリア条件が紹介されてる状態か
    /// </summary>
    /// <returns></returns>
    public bool IsIntroduceClear()
    {
        return CurrentState == GameState.IntroduceClear;
    }
    /// <summary>
    /// ゲームプレイ中か
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        return CurrentState == GameState.Playing;
    }
    /// <summary>
    /// ポーズ中か
    /// </summary>
    /// <returns></returns>
    public bool IsPause()
    {
        return CurrentState == GameState.Pause;
    }
    /// <summary>
    /// 結果中か
    /// </summary>
    /// <returns></returns>
    public bool IsResult()
    {
        return CurrentState == GameState.Result;
    }
}
