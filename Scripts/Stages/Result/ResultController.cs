using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルト処理を管理し、走らせるコントローラー
/// </summary>
public class ResultController : MonoBehaviour
{
    [Header("リザルト機能の参照")]
    [Tooltip("リザルトの条件を計算する際に判定")]
    [SerializeField] private ResultCalculation resultCalculation;
    [Tooltip("リザルト画面のUIやアニメーション表示を担当")]
    [SerializeField] private ResultView resultView;
    [Tooltip("机の下への落下判定を担当")]
    [SerializeField] private ResultUnderdesk resultUnderdesk;

    //リザルトが二重に実行されるのを防ぐ
    private bool isResult = false;

    private void Awake()
    {
        if(resultCalculation == null) { TryGetComponent<ResultCalculation>(out resultCalculation); }
        if(resultView == null) { TryGetComponent<ResultView>(out resultView); }
        if (resultUnderdesk == null) { Debug.LogError("resultUnderdeskが参照されていません。"); return; }
    }

    private void OnEnable()
    {
        if (resultUnderdesk != null)
        {
            resultUnderdesk.OnItemFall += FallObject;
        }
    }

    private void OnDisable()
    {
        resultUnderdesk.OnItemFall -= FallObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isResult) return;

        //何かしらが落ちたらリザルトに入る
        isResult = true;

        //餅が落ちる
        if(other.gameObject.TryGetComponent<IRiceCake>(out var _ricecake))
        {
            //現在の餅の状態を知らせる
            resultCalculation.SetNowCondition(_ricecake);

            StageStateController.Instance.ChangeState(StageState.GAME_END);
            other.gameObject.SetActive(false);
        }

        //ホッピングが落ちる
        else if(other.gameObject.TryGetComponent<HoppingController>(out var _hopping))
        {
            //食べれないリザルト
            //自身の餅の状態を知る必要はないので、直接リザルト遷移に入る
            resultView.SomethingGotInAnimationAsync();

            StageStateController.Instance.ChangeState(StageState.GAME_END);
        }
    }

    /// <summary>
    /// アイテムが落ちたら関数を通して予備だす(Unitaskは直接は呼び出せるが、こちらの方が見やすい)
    /// </summary>
    private void FallObject()
    {
        //食べれないリザルト
        resultView.NotEatAnimationAsync().Forget();
    }
}
