using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

/// <summary>
/// リザルト画面をアニメーションで表示
/// </summary>
public class ResultView : MonoBehaviour
{
    [SerializeField] private HideStageObject hideStage;
    [SerializeField] private ResultCamera resultCamera;

    [SerializeField] private GameObject resultUIObject;

    private float waitTimeBeforeResult = 2.5f;
    private float delayBeforeTextVisible = 0.5f;
    private float notEatAnimationDuration = 5f / 6f;
    private float notEatWaitTime = 0.2f;
    
    private bool isResult = false;

    [SerializeField] private Animator resultAnimator;
    // トリガー名を定数として管理
    private const string TRIGGER_EAT = "Eat";
    private const string TRIGGER_NOT_EAT = "NotEat";
    //結果のトリガー名
    private const string TRIGGER_VERY_DELICIOUS = "VeryDelicious";
    private const string TRIGGER_SO_DELICIOUS = "SoDelicious";
    private const string TRIGGER_TOO_MUCH = "TooMuch";
    private const string TRIGGER_NOT_ENOUGH = "NotEnough";
    private const string TRIGGER_SOMETHING_GOT_IN = "SomethingGotIn";

    // Start is called before the first frame update
    void Awake()
    {
        if(hideStage == null) { TryGetComponent<HideStageObject>(out hideStage); }
        if(resultAnimator == null) { Debug.LogError("resultAnimatorが参照されていません"); return; }
        
        if(resultUIObject == null) { Debug.LogError("resultUIObjectが参照されていません"); return; }
    }
    //しっかりと提供できた
    public async UniTaskVoid VeryDeliciousAnimationAsync()
    {
        var _token = this.GetCancellationTokenOnDestroy();
        await EatAnimationAsync(_token);
        SoundManager.Instance.PlaySE(SESource.veryDelicious);
        resultAnimator.SetTrigger(TRIGGER_VERY_DELICIOUS);
        DisplayUIAsync(_token).Forget();
    }

    //味は違うけどサイズが正しい
    public async UniTaskVoid SoDeliciousAnimationAsync()
    {
        var _token = this.GetCancellationTokenOnDestroy();
        await EatAnimationAsync(_token);
        SoundManager.Instance.PlaySE(SESource.soDelicious);
        resultAnimator.SetTrigger(TRIGGER_SO_DELICIOUS);
        DisplayUIAsync(_token).Forget();
    }

    //サイズが多すぎるとき
    public async UniTaskVoid TooMuchAnimationAsync()
    {
        var _token = this.GetCancellationTokenOnDestroy();
        await EatAnimationAsync(_token);
        SoundManager.Instance.PlaySE(SESource.tooMuch);
        resultAnimator.SetTrigger(TRIGGER_TOO_MUCH);
        DisplayUIAsync(_token).Forget();
    }

    //サイズが十分で無いとき
    public async UniTaskVoid NotEnoughAnimationAsync()
    {
        var _token = this.GetCancellationTokenOnDestroy();
        await EatAnimationAsync(_token);
        SoundManager.Instance.PlaySE(SESource.notEnough);
        resultAnimator.SetTrigger(TRIGGER_NOT_ENOUGH);
        DisplayUIAsync(_token).Forget();
    }

    //何か入った時
    public async UniTaskVoid SomethingGotInAnimationAsync()
    {
        var _token = this.GetCancellationTokenOnDestroy();
        await EatAnimationAsync(_token);
        SoundManager.Instance.PlaySE(SESource.somethingGotIn);
        resultAnimator.SetTrigger(TRIGGER_SOMETHING_GOT_IN);
        DisplayUIAsync(_token).Forget();
    }

    //食べてる時のアニメーション
    private async UniTask EatAnimationAsync(CancellationToken _token)
    {
        if(isResult) return;

        isResult = true;
        SoundManager.Instance.PlaySE(SESource.eat);
        hideStage.AllHideStageObj();
        resultCamera.EatCamera();
        resultAnimator.SetTrigger(TRIGGER_EAT);

        //もぐもぐタイム
        await UniTask.WaitForSeconds(waitTimeBeforeResult, cancellationToken: _token);
    }

    //食えないときのアニメーション
    public async UniTaskVoid NotEatAnimationAsync()
    {
        var _token = this.GetCancellationTokenOnDestroy();
        await TableFlipAnimationAsync(_token);
        DisplayUIAsync(_token).Forget();
    }

    //ちゃぶだい返しのアニメーション
    private async UniTask TableFlipAnimationAsync(CancellationToken _token)
    {
        if (isResult) return;

        isResult = true;

        hideStage.FailedResultHideStageObj();
        resultCamera.NotEatCamera();

        await UniTask.WaitForSeconds(notEatWaitTime, cancellationToken: _token);
        resultAnimator.SetTrigger(TRIGGER_NOT_EAT);
        SoundManager.Instance.PlaySE(SESource.notEat);

        await UniTask.WaitForSeconds(notEatAnimationDuration, cancellationToken: _token);
    }
    public async UniTaskVoid DisplayUIAsync(CancellationToken _token)
    {
        await UniTask.WaitForSeconds(delayBeforeTextVisible, cancellationToken: _token);
        if (resultUIObject != null) resultUIObject.SetActive(true);
    }
}
