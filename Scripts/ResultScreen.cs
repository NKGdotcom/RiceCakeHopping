using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// リザルト画面(UIページ画面)
/// </summary>
public class ResultScreen : TextColorChange
{
    protected SetClearConditions setClearConditions;

    [Header("フェードアウト用のアニメーション、上のものは入れなくて大丈夫")]
    [SerializeField] protected Animator fadeOutAnimator;

    protected float stageNum;
    protected float fadeOutWaitTime = 0.5f;
    // Start is called before the first frame update
    new protected virtual void Start()
    {
        setClearConditions = GetComponent<SetClearConditions>();
        stageNum = setClearConditions.StageIndexNum;
        SetUpTextEvent(GameResult.Instance.ResultNextSceneTMP.gameObject, //次のステージへ
                        (eventData) => { ChangeBlackText(GameResult.Instance.ResultNextSceneTMP); },
                        (eventData) => { ResetBlackText(GameResult.Instance.ResultNextSceneTMP); },
                        (eventData) => {
                            StartCoroutine(ChangeScene($"Stage{stageNum + 1}"));
                        });
        SetUpTextEvent(GameResult.Instance.ResultOneMoreTimeTMP.gameObject, //もう一度同じステージ
                        (eventData) => { ChangeBlackText(GameResult.Instance.ResultOneMoreTimeTMP); },
                        (eventData) => { ResetBlackText(GameResult.Instance.ResultOneMoreTimeTMP); },
                        (eventData) => {
                            StartCoroutine(ChangeScene($"Stage{stageNum}"));
                        });
        SetUpTextEvent(GameResult.Instance.ResultTitleTMP.gameObject, //タイトルへ戻る
                        (eventData) => { ChangeBlackText(GameResult.Instance.ResultTitleTMP); },
                        (eventData) => { ResetBlackText(GameResult.Instance.ResultTitleTMP); },
                        (eventData) => {
                            StartCoroutine(ChangeScene("Title"));
                        });
    }

    protected IEnumerator ChangeScene(string _stage)
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        fadeOutAnimator.SetBool("FadeOut",true);
        yield return new WaitForSeconds(fadeOutWaitTime);
        SceneManager.LoadScene(_stage);
    }
}
