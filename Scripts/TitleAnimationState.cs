using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAnimationState : MonoBehaviour
{
    public static TitleAnimationState Instance { get; private set; }

    [SerializeField] private Animator titleAnimator;
    private float fadeOutSpeed = 0.5f;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    /// <summary>
    /// タイトルの後、ステージ選択か遊び方のページかを選ぶ
    /// (ステージ選択か遊び方のページに戻る際も有効)
    /// </summary>
    public void MoveToTitleNext()
    {
        titleAnimator.SetTrigger("TapAnyKey");
        titleAnimator.SetBool("GoToStageSelect", false);
        titleAnimator.SetBool("GoToHowToPlay", false);
    }

    /// <summary>
    /// ステージ選択画面に移動
    /// </summary>
    public void MoveToStageSelectPage()
    {
        titleAnimator.SetBool("GoToStageSelect", true);
    }

    /// <summary>
    /// 遊び方ページに移動
    /// </summary>
    public void MoveToHowToPlayPage()
    {
        titleAnimator.SetBool("GoToHowToPlay", true);
    }

    /// <summary>
    /// ゲーム開始準備
    /// </summary>
    /// <param name="_stageName">移るステージ名</param>
    public void GameStart(string _stageName)
    {
        titleAnimator.SetTrigger("GameStart");
        StartCoroutine(WaitStart(_stageName));
    }

    /// <summary>
    /// ゲーム開始
    /// </summary>
    /// <param name="_stageName">移るステージ名</param>
    /// <returns></returns>
    private IEnumerator WaitStart(string _stageName)
    {
        yield return new WaitForSeconds(fadeOutSpeed);
        SceneManager.LoadScene(_stageName);
    }
}
