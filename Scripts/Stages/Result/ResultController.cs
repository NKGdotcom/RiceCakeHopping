using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルト画面のトリガー用
/// </summary>
public class ResultController : MonoBehaviour
{
    [SerializeField] private ResultCalculation resultCalculation;
    [SerializeField] private ResultView resultView;
    [SerializeField] private ResultUnderdesk resultUnderdesk;
    private bool isResult = false;

    private void Awake()
    {
        if(resultCalculation == null) { TryGetComponent<ResultCalculation>(out resultCalculation); }
        if(resultView == null) { TryGetComponent<ResultView>(out resultView); }
        if (resultUnderdesk == null) { Debug.LogError("resultUnderdeskが参照されていません。"); return; }

        resultUnderdesk.IsItemFall += FallObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (resultView == null) return;
        if (isResult) return;
        isResult = true;

        if(other.gameObject.TryGetComponent<IRiceCake>(out var _ricecake))
        {
            resultCalculation.SetNowCondition(_ricecake);
            StageStateController.Instance.ChangeState(StageState.GAME_END);
        }
        else if(other.gameObject.TryGetComponent<HoppingController>(out var _hopping))
        {
            resultView.SomethingGotInAnimationAsync().Forget();
            StageStateController.Instance.ChangeState(StageState.GAME_END);
        }
    }

    public void FallObject()
    {
        resultView.NotEatAnimationAsync().Forget();
    }

    private void OnDestroy()
    {
        resultUnderdesk.IsItemFall -= FallObject;
    }
}
