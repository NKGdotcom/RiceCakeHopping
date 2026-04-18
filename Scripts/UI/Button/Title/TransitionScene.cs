using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンの遷移を行い、ステージの切り替えを行う
/// </summary>
public class TransitionScene : MonoBehaviour
{
    [Header("ステージ遷移")]
    [Tooltip("次に進むステージ名を知らせる")]
    [SerializeField] private string nextStage = "Stage";
    private const string STR_TITLE = "Title";

    /// <summary>
    /// 設定したステージに遷移
    /// </summary>
    /// <param name="_toStage"></param>
    public void ToSelectStage(string _toStage)
    {
        SceneManager.LoadScene(_toStage);
    }

    /// <summary>
    /// もう一度同じステージをやり直す
    /// </summary>
    public void ToRetryStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 次のステージに進む
    /// </summary>
    public void ToNextStage()
    {
        SceneManager.LoadScene(nextStage);
    }

    /// <summary>
    /// 現在のステージからタイトルに戻る
    /// </summary>
    public void ToTitle()
    {
        Debug.Log("Title");
        SceneManager.LoadScene(STR_TITLE);
    }
}
