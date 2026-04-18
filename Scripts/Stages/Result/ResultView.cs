using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System.Threading;
using UnityEngine;

/// <summary>
/// リザルト画面のUIやアニメーションの演出を管理する
/// </summary>
public class ResultView : MonoBehaviour
{
    [Header("他のコンポーネントの参照")]
    [Tooltip("ステージ内で削除するオブジェクト")]
    [SerializeField] private HideStageObject hideStage;
    [Tooltip("リザルトのカメラ演出")]
    [SerializeField] private ResultCamera resultCamera;

    [Header("UIとアニメーション")]
    [Tooltip("最終的に表示するリザルト画面のUI本体")]
    [SerializeField] private GameObject resultUIObject;
    [Tooltip("評価演出を行うアニメーター")]
    [SerializeField] private Animator resultAnimator;

    [Header("演出のタイミング調整")]
    [Tooltip("もぐもぐタイム(食べる～評価が出るまで)の秒数")]
    [SerializeField] private float eatingDuration = 2.5f;
    [Tooltip("アニメーションを再生し、UIが出るまでの秒数")]
    [SerializeField] private float uiDisplayDelay = 0.5f;
    [Tooltip("カメラが引き切り、ちゃぶ台がすべて見えるまで待つ")]
    [SerializeField] private float preTableFlipDelay = 0.2f;
    [Tooltip("ちゃぶ台を返した後、UIを表示させる")]
    [SerializeField] private float tableFlipDuration = 5f / 6f;
    
    //演出が重複して走るのを防ぐ
    private bool isPlayingSequence = false;

    // アニメーションのトリガー名を定数として管理
    private const string TRIGGER_EAT = "Eat";
    private const string TRIGGER_NOT_EAT = "NotEat";
    private const string TRIGGER_VERY_DELICIOUS = "VeryDelicious";
    private const string TRIGGER_SO_DELICIOUS = "SoDelicious";
    private const string TRIGGER_TOO_MUCH = "TooMuch";
    private const string TRIGGER_NOT_ENOUGH = "NotEnough";
    private const string TRIGGER_SOMETHING_GOT_IN = "SomethingGotIn";

    // Start is called before the first frame update
    void Awake()
    {
        if(hideStage == null) { Debug.LogError("hideStageが参照されていません"); return; }
        if(resultCamera == null) { Debug.LogError("resultCameraが参照されていません"); return; }
        if(resultUIObject == null) { Debug.LogError("resultUIObjectが参照されていません"); return; }
        if (resultAnimator == null) { Debug.LogError("resultAnimatorが参照されていません"); return; }
    }

    /// <summary>
    /// 適切な餅の味、餅のサイズを提供できた
    /// </summary>
    public void VeryDeliciousAnimationAsync()
    {
        ExcuteEvaluationAsync(SESource.VERY_DELICIOUS, TRIGGER_VERY_DELICIOUS).Forget();
    }

    /// <summary>
    /// 異なる餅の味、適切な餅のサイズを提供した
    /// </summary>
    public void SoDeliciousAnimationAsync()
    {
        ExcuteEvaluationAsync(SESource.SO_DELICIOUS, TRIGGER_SO_DELICIOUS).Forget();
    }

    /// <summary>
    /// 大きい餅のサイズを提供した
    /// </summary>
    public void TooMuchAnimationAsync()
    {
        ExcuteEvaluationAsync(SESource.TOO_MUCH, TRIGGER_TOO_MUCH).Forget();
    }

    /// <summary>
    /// 小さな餅のサイズを提供したい
    /// </summary>
    public void NotEnoughAnimationAsync()
    {
        ExcuteEvaluationAsync(SESource.NOT_ENOUGH, TRIGGER_NOT_ENOUGH).Forget();
    }

    /// <summary>
    /// 餅ではなく、ホッピングを提供した
    /// </summary>
    public void SomethingGotInAnimationAsync()
    {
        ExcuteEvaluationAsync(SESource.SOMETHING_GOT_IN, TRIGGER_SOMETHING_GOT_IN).Forget();
    }

    /// <summary>
    /// 食べてから口から評価を出し、UIを表示するための一連の流れ
    /// </summary>
    /// <param name="_seSource"></param>
    /// <param name="_triggerName"></param>
    /// <returns></returns>
    private async UniTaskVoid ExcuteEvaluationAsync(SESource _seSource, string _triggerName)
    {
        var _token = this.GetCancellationTokenOnDestroy();

        //もぐもぐ食べる待ち時間
        await EatAnimationAsync(_token);

        //リザルトで口から評価を出すアニメーション
        SoundManager.Instance.PlaySE(_seSource);
        resultAnimator.SetTrigger(_triggerName);

        //UIをタイミングよく表示
        DisplayUIAsync(_token).Forget();
    }

    /// <summary>
    /// 机の下に餅やホッピングを落ちたときのアニメーション
    /// </summary>
    /// <returns></returns>
    public async UniTaskVoid NotEatAnimationAsync()
    {
        var _token = this.GetCancellationTokenOnDestroy();

        //ちゃぶだい返しが終わるまで待つ
        await TableFlipAnimationAsync(_token);

        //次に進む等のUIを表示
        DisplayUIAsync(_token).Forget();
    }

    /// <summary>
    /// もぐもぐ食べてる時のアニメーション
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    private async UniTask EatAnimationAsync(CancellationToken _token)
    {
        if (isPlayingSequence) return;
        isPlayingSequence = true;

        SoundManager.Instance.PlaySE(SESource.EAT);
        //リザルトを見やすくするために非表示
        hideStage.AllHideStageObj();

        //もぐもぐの演出を見せる
        resultCamera.EatCamera();
        resultAnimator.SetTrigger(TRIGGER_EAT);

        //もぐもぐタイム
        await UniTask.WaitForSeconds(eatingDuration, cancellationToken: _token);
    }

    /// <summary>
    /// ちゃぶだい返しのアニメーション
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    private async UniTask TableFlipAnimationAsync(CancellationToken _token)
    {
        if (isPlayingSequence) return;
        isPlayingSequence = true;

        //画面に映っているちゃぶ台の上のすべてのオブジェクトを消す
        hideStage.FailedResultHideStageObj();
        //食えない時のリザルトカメラに遷移準備
        resultCamera.NotEatCamera();

        //カメラが引ききり、ちゃぶ台が全部見えるまで待機
        await UniTask.WaitForSeconds(preTableFlipDelay, cancellationToken: _token);
        
        //アニメーション遷移
        resultAnimator.SetTrigger(TRIGGER_NOT_EAT);
        SoundManager.Instance.PlaySE(SESource.NOT_EAT);

        //ちゃぶだい返しのアニメーションの終了を待つ
        await UniTask.WaitForSeconds(tableFlipDuration, cancellationToken: _token);
    }

    /// <summary>
    /// ボタンUI(次のステージ、もう一度、タイトル)を表示させる
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    private async UniTaskVoid DisplayUIAsync(CancellationToken _token)
    {
        await UniTask.WaitForSeconds(uiDisplayDelay, cancellationToken: _token);
        if (resultUIObject != null) resultUIObject.SetActive(true);
    }
}
