using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum ResultType
{
    none,
    VeryDelicious, // 完璧
    SoDelicious,   // 味違い
    TooMuch,       // 多い
    NotEnough,     // 少ない
    SomethingGotIn,// 異物
    NotEat         // 食べられない
}

public class ResultManager : MonoBehaviour
{
    //カメラ移動を行う
    [SerializeField] private CinemachineVirtualCamera resultCamera;
    [SerializeField] private CinemachineVirtualCamera notEatResultCamera;
    private const int ActivePriority = 20;
    private const int InactivePriority = 10;

    //animationのトリガー
    [SerializeField] private Animator resultAnimator;
    // トリガー名を定数として管理
    private const string TriggerEat = "Eat";
    private const string TriggerNotEat = "NotEat";
    // 結果ごとのトリガー名
    private const string TriggerVeryDelicious = "VeryDelicious";
    private const string TriggerSoDelicious = "SoDelicious";
    private const string TriggerTooMuch = "TooMuch";
    private const string TriggerNotEnough = "NotEnough";
    private const string TriggerSomethingGotIt = "SomethingGotIn";

    [SerializeField] private AllHideObjResult allHideObjResult;
    //テキスト
    [SerializeField] private GameObject resultUIParent;

    [SerializeField] private float waitTimeBeforeResult = 2.5f;
    [SerializeField] private float delayBeforeTextVisible = 0.5f;
    private float notEatAnimationDuration = 5 / 6f;
    [SerializeField] private float notEatWaitTime = 0.2f;
    private SESource resultSeSource;
    private bool isResult;

    private void Start()
    {
        resultUIParent.SetActive(false);
    }
    /// <summary>
    /// 外部（StageManager）からはこれを呼ぶだけでOKにします
    /// </summary>
    public void ShowResult(ResultType type)
    {
        StartCoroutine(ResultSequence(type));
    }

    /// <summary>
    /// 結果演出のメインコルーチン（一本化）
    /// </summary>
    private IEnumerator ResultSequence(ResultType type)
    {
        if (isResult) yield break;

        isResult = true;
        // 1. 「食べられない」パターンと「食べる」パターンで分岐
        if (type == ResultType.NotEat)
        {
            // --- 食べられない場合 ---
            allHideObjResult.FailedResultHideObj();
            if (notEatResultCamera != null) notEatResultCamera.Priority = ActivePriority;

            yield return new WaitForSeconds(notEatWaitTime);

            SoundManager.Instance.PlaySE(SESource.notEat);
            resultAnimator.SetTrigger(TriggerNotEat);

            // アニメーションの長さ分待つ
            yield return new WaitForSeconds(notEatAnimationDuration);
        }
        else
        {
            // --- 食べる場合（共通処理） ---
            allHideObjResult.AllHideObj();
            if (resultCamera != null) resultCamera.Priority = ActivePriority;
            resultAnimator.SetTrigger(TriggerEat);

            SoundManager.Instance.PlaySE(SESource.eat);
            // もぐもぐタイム
            yield return new WaitForSeconds(waitTimeBeforeResult);

            // タイプに応じた 音 と アニメーション を決定
            PlayResultReaction(type);
            SoundManager.Instance.PlaySE(resultSeSource);
        }

        // 共通：UI表示前のタメ
        yield return new WaitForSeconds(delayBeforeTextVisible);

        // 共通：UI表示
        if (resultUIParent != null) resultUIParent.SetActive(true);
    }

    /// <summary>
    /// 結果タイプに応じた音とアニメーションを実行
    /// </summary>
    private void PlayResultReaction(ResultType type)
    {
        string triggerToSet = "";

        switch (type)
        {
            case ResultType.VeryDelicious:
                triggerToSet = TriggerVeryDelicious;
                resultSeSource = SESource.veryDelicious;
                break;
            case ResultType.SoDelicious:
                triggerToSet = TriggerSoDelicious;
                resultSeSource = SESource.soDelicious;
                break;
            case ResultType.TooMuch:
                triggerToSet = TriggerTooMuch;
                resultSeSource = SESource.tooMuch;
                break;
            case ResultType.NotEnough:
                triggerToSet = TriggerNotEnough;
                resultSeSource = SESource.notEnough;
                break;
            case ResultType.SomethingGotIn:
                triggerToSet = TriggerSomethingGotIt;
                resultSeSource = SESource.somethingGotIn;
                break;
        }
        if (!string.IsNullOrEmpty(triggerToSet)) resultAnimator.SetTrigger(triggerToSet);
    }
}
