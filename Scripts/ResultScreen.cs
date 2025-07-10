using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// リザルト画面(UIページ画面)
/// </summary>
public class ResultScreen : UIPageMove
{
    protected SetClearConditions setClearConditions;

    [Header("フェードアウト用のアニメーション、上のものは入れなくて大丈夫")]
    [SerializeField] protected Animator fadeOutAnimator;
    protected static readonly Color32 highlightColor = new Color32(255, 130, 130, 255);//テキストの上に置いたら少し赤っぽい色に
    protected static readonly Color32 defaultColor = new Color32(0, 0, 0, 255);//テキストから外れた時

    protected float stageNum;
    protected float fadeOutWaitTime = 0.5f;
    // Start is called before the first frame update
    new protected virtual void Start()
    {
        setClearConditions = GetComponent<SetClearConditions>();
        stageNum = setClearConditions.StageIndexNum;
        SetUpTextEvent(GameResult.Instance.ResultNextSceneTMP.gameObject, //次のステージへ
                        (eventData) => { StartColorChange(GameResult.Instance.ResultNextSceneTMP, highlightColor); },
                        (eventData) => { StartColorChange(GameResult.Instance.ResultNextSceneTMP, defaultColor); },
                        (eventData) => {
                            StartCoroutine(ChangeScene($"Stage{stageNum + 1}"));
                        });
        SetUpTextEvent(GameResult.Instance.ResultOneMoreTimeTMP.gameObject, //もう一度同じステージ
                        (eventData) => { StartColorChange(GameResult.Instance.ResultOneMoreTimeTMP, highlightColor); },
                        (eventData) => { StartColorChange(GameResult.Instance.ResultOneMoreTimeTMP, defaultColor); },
                        (eventData) => {
                            StartCoroutine(ChangeScene($"Stage{stageNum}"));
                        });
        SetUpTextEvent(GameResult.Instance.ResultTitleTMP.gameObject, //タイトルへ戻る
                        (eventData) => { StartColorChange(GameResult.Instance.ResultTitleTMP, highlightColor); },
                        (eventData) => { StartColorChange(GameResult.Instance.ResultTitleTMP, defaultColor); },
                        (eventData) => {
                            StartCoroutine(ChangeScene("Title"));
                        });
    }

    protected IEnumerator ChangeScene(string _stage)
    {
        fadeOutAnimator.SetBool("FadeOut",true);
        yield return new WaitForSeconds(fadeOutWaitTime);
        SceneManager.LoadScene(_stage);
    }
}
