using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クリア条件のbool等をここでたてる
/// </summary>
public class ResultCalculation : MonoBehaviour
{
    private StageData stageData;
    [SerializeField] private ResultView resultView;
    [SerializeField] private TimeController timeController;

    private RicecakeType nowRicecakeType;
    private float nowRicecakeSize;

    private void Awake()
    {
        if(resultView == null) { Debug.LogError("resultViewが参照されていません"); return; }
        if(timeController == null) { Debug.LogError("timeControllerが参照されていません"); return; }
    }

    public void SetResultCondition(StageData _stageData)
    {
        stageData = _stageData;
    }

    public void SetNowCondition(IRiceCake _ricecake)
    {
        nowRicecakeType = _ricecake.MyType;
        nowRicecakeSize = _ricecake.RicecakeSize;
        Debug.Log(nowRicecakeType);
        Debug.Log(nowRicecakeSize);
        CheckResult();
    }

    //現在の自分の状態とリザルト条件を確認
    private void CheckResult()
    {
        if (IsRightTypeSize()) resultView.VeryDeliciousAnimationAsync().Forget();
        else if (IsMissTypeRightSize()) resultView.SoDeliciousAnimationAsync().Forget();
        else if(IsRightTypeLargeSize()) resultView.TooMuchAnimationAsync().Forget();
        else if(IsRightTypeSmallSize()) resultView.NotEnoughAnimationAsync().Forget();
    }

    //正しいタイプ
    private bool IsRightTypeSize()
    {
        return (nowRicecakeType == stageData.TargetRicecakeType) && (nowRicecakeSize == stageData.TargetSize);
    }

    //タイプは異なるが大きさはあっている、
    private bool IsMissTypeRightSize()
    {
        return (nowRicecakeType != stageData.TargetRicecakeType) && (nowRicecakeSize == stageData.TargetSize);
    }

    //タイプはあっているが本来より大きいサイズ
    private bool IsRightTypeLargeSize()
    {
        return (nowRicecakeSize > stageData.TargetSize);
    }

    //タイプはあっているが本来より小さいサイズ
    private bool IsRightTypeSmallSize()
    {
        return (nowRicecakeSize < stageData.TargetSize);
    }
}
