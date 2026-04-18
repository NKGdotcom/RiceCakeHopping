using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クリア条件の比較を行う
/// </summary>
public class ResultCalculation : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("アニメーションでリザルト演出を再生するクラス")]
    [SerializeField] private ResultView resultView;
    [Tooltip("制限時間が0になったら食えないじゃないかを呼び出すために使用")]
    [SerializeField] private TimeController timeController;

    //現在のステージのクリア条件を持たせる
    private StageData stageData;

    //今の餅の味付けとサイズ
    private RicecakeType nowRicecakeType;
    private float nowRicecakeSize;

    private void Awake()
    {
        if(resultView == null) { Debug.LogError("resultViewが参照されていません"); return; }
        if(timeController == null) { Debug.LogError("timeControllerが参照されていません"); return; }
    }

    /// <summary>
    /// クリア条件を設定
    /// </summary>
    /// <param name="_stageData"></param>
    public void SetResultCondition(StageData _stageData)
    {
        stageData = _stageData;
    }

    /// <summary>
    /// 現在の餅の状態を記憶
    /// </summary>
    /// <param name="_ricecake"></param>
    public void SetNowCondition(IRiceCake _ricecake)
    {
        //餅の状態を記憶
        nowRicecakeType = _ricecake.MyType;
        nowRicecakeSize = _ricecake.RicecakeSize;

        //リザルトと比較する
        CheckResult();
    }

    /// <summary>
    /// 現在の自分の状態とリザルト条件を確認
    /// </summary>
    private void CheckResult()
    {
        //味付けと大きさが同じ
        if (IsRightTypeSize()) resultView.VeryDeliciousAnimationAsync();
        
        //味付けは違うが、大きさが同じ
        else if (IsMissTypeRightSize()) resultView.SoDeliciousAnimationAsync();
        
        //味付けは合っているが、大きさが大きい
        else if(IsRightTypeLargeSize()) resultView.TooMuchAnimationAsync();
        
        //味付けは合っているが、大きさが小さい
        else if(IsRightTypeSmallSize()) resultView.NotEnoughAnimationAsync();
    }

    /// <summary>
    /// 味付けサイズが同じ
    /// </summary>
    /// <returns></returns>
    private bool IsRightTypeSize()
    {
        return (nowRicecakeType == stageData.TargetRicecakeType) && (nowRicecakeSize == stageData.TargetSize);
    }

    /// <summary>
    /// 味付けは異なるが大きさが合っている
    /// </summary>
    /// <returns></returns>
    private bool IsMissTypeRightSize()
    {
        return (nowRicecakeType != stageData.TargetRicecakeType) && (nowRicecakeSize == stageData.TargetSize);
    }

    /// <summary>
    /// 味付けはあっているが要求よりも大きいサイズ
    /// </summary>
    /// <returns></returns>
    private bool IsRightTypeLargeSize()
    {
        return (nowRicecakeSize > stageData.TargetSize);
    }

    /// <summary>
    /// 味付けはあっているが要求より小さいサイズ
    /// </summary>
    /// <returns></returns>
    private bool IsRightTypeSmallSize()
    {
        return (nowRicecakeSize < stageData.TargetSize);
    }
}
