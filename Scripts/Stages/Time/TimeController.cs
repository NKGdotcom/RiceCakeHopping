using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// êßå¿éûä‘ÇÃëÄçÏ
/// </summary>
public class TimeController : MonoBehaviour
{
    [SerializeField] private TimeView timeView;
    [SerializeField] private ResultView resultView;
    private float waitTime = 2;
    private float timer;
    private bool isFinised = false;

    private void Awake()
    {
        if(timeView == null) { TryGetComponent<TimeView>(out timeView); }
        WaitStart().Forget();
    }

    private async UniTaskVoid WaitStart()
    {
        StageStateController.Instance.ChangeState(StageState.GAME_READY);
        var _token = this.GetCancellationTokenOnDestroy();
        await UniTask.WaitForSeconds(waitTime, cancellationToken: _token);
        StageStateController.Instance.ChangeState(StageState.GAME_PLAY);
    }

    // Update is called once per frame
    void Update()
    {
        if (StageStateController.Instance.IsPlaying())
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                timeView.UpdateTMP(timer);
            }
            else if (timer < 0 && !isFinised)
            {
                isFinised = true;
                timer = 0;
                resultView.NotEatAnimationAsync().Forget();
            }
        }
    }

    public void SetupTime(float _limitTime)
    {
        timer = _limitTime;
    }
}
