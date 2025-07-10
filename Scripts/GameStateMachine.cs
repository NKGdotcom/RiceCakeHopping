using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���̏�Ԃ�ݒ�
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
    /// ��Ԃ��X�V
    /// </summary>
    /// <param name="_state">�ύX���������</param>
    public void SetState(GameState _state)
    {
        CurrentState = _state;
    }

    /// <summary>
    /// �N���A�������Љ��Ă��Ԃ�
    /// </summary>
    /// <returns></returns>
    public bool IsIntroduceClear()
    {
        return CurrentState == GameState.IntroduceClear;
    }
    /// <summary>
    /// �Q�[���v���C����
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        return CurrentState == GameState.Playing;
    }
    /// <summary>
    /// �|�[�Y����
    /// </summary>
    /// <returns></returns>
    public bool IsPause()
    {
        return CurrentState == GameState.Pause;
    }
    /// <summary>
    /// ���ʒ���
    /// </summary>
    /// <returns></returns>
    public bool IsResult()
    {
        return CurrentState == GameState.Result;
    }
}
