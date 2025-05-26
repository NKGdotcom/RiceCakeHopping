using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using static SoundManager;

public class StageManager : MonoBehaviour
{
    public int StageNum { get => stageNum; set => stageNum = value; }
    public bool IsPause { get => isPause; set => isPause = value; }
    public bool IsResult { get => isResult; set => isResult = value; }

    [Header("他の参照クラス")]
    [SerializeField] private ClearConditions clearConditions;
    [SerializeField] private RiceCakeManager riceCakeManager;

    [Header("シーン内のUI関連")]
    [SerializeField] private TextMeshProUGUI timerTextMeshPro;
    [SerializeField] private TextMeshProUGUI clearConditionTextMeshPro; //クリア条件を示すテキスト
    
    [Header("リザルト時のUI関連")]
    [SerializeField] private TextMeshProUGUI resultNextStageTMP;
    [SerializeField] private TextMeshProUGUI resultOneMoreTimeTMP;
    [SerializeField] private TextMeshProUGUI resultTitleTMP;

    [Header("制限時間や時間関連")]
    [SerializeField] private float timeLimit = 60; //制限時間
    private float timeLimitBoundary = 0;
    private string timeLimitString = "0:00";
    [SerializeField] private float waitTime = 2.5f; //クリア条件を見せるまで操作できない時間
    private float waitTimeBoundary = 0;
    private const float oneMinute = 60;
    private float minutes;
    private float seconds;

    [Header("リザルト時に消すオブジェクトたち")]
    [SerializeField] private GameObject hopping;
    [SerializeField] private GameObject table;
    [SerializeField] private List<GameObject> otherObjectList; //それ以外

    [Header("カメラ移動を行う変数")]
    //カメラ移動を行う
    [SerializeField] private CinemachineVirtualCamera resultCamera;
    [SerializeField] private CinemachineVirtualCamera notEatResultCamera;
    private int resultCameraPriority = 20;
    private int defaultCameraPriority = 10;

    [Header("おばあちゃんのクリア時のアニメーター")]
    [SerializeField] private Animator grondMomAndResultAnimator;
    //animationのトリガー
    private string eatTrigger = "Eat";
    private string noteatTrigger = "NotEat";
    private string veryDeliciousTrigger = "VeryDelicious";
    private string soDeliciousTrigger = "SoDelicious";
    private string toomuchTrigger = "TooMuch";
    private string notEnoughTrigger = "NotEnough";
    private string somethingGotItTrigger = "SomethingGotIt";

    [Header("クリア条件")]
    [SerializeField] private ClearConditions.ClearCondition.Conditions conditionType;
    private string clearConditionTagName;
    private string clearConditionTextString;
    private float clearConditionRiceCakeSize;

    private float waitTimeBeforeResult = 2.5f;
    private float delayBeforeTextVisible = 0.5f;
    private float notEatAnimationDuration = 5 / 6f;
    private float noteatWaitTime = 0.2f;

    private bool isGamePlay = false; //プレイ中か
    private bool isPause = false; //ポーズ中か
    private bool isResult = false; //リザルトへ
    private bool justOneResult = false; //SEが鳴りすぎないように

    private int stageNum;
    /// <summary>
    /// 餅のリザルト
    /// </summary>
    private enum MochiResultType
    {
        PerfectMatch,
        WrongTagSameSize,
        WrongTagTooBig,
        WrongTagTooSmall,
        CorrectTagTooBig,
        CorrectTagTooSmall,
        PlayerCollision
    }
    // Start is called before the first frame update
    void Start()
    {
        SetClearCondition(conditionType);//クリア条件を参照

        SoundManager.Instance.PlayBGM(BGMSource.stageBGM);

        isGamePlay = false;
        isPause = false;
        isResult = false;
        justOneResult = false;

        Debug.Log("timeLimit: " + timeLimit);
        Debug.Log("timeLimitBoundary: " + timeLimitBoundary);
        seconds = Mathf.Floor(Mathf.Repeat(timeLimit, oneMinute));
        minutes = Mathf.Floor((timeLimit) / oneMinute);
        timerTextMeshPro.SetText(minutes.ToString("f0") + ":" + seconds.ToString("00"));//制限時間を設定
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePlay)//初めのクリア条件を示す時間
        {
            if(waitTime > waitTimeBoundary)
            {
                waitTime -= Time.deltaTime;
            }
            else if(waitTime < waitTimeBoundary)
            {
                isGamePlay = true;
                waitTime = waitTimeBoundary;
            }
        }
        else//ゲーム中
        {
            if (!isPause)//ポーズ画面に行っていない時
            {
                timeLimit -= Time.deltaTime;
                if (timeLimit > timeLimitBoundary)
                {
                    seconds = Mathf.Floor(Mathf.Repeat(timeLimit + 1, oneMinute));
                    minutes = Mathf.Floor((timeLimit + 1) / oneMinute);
                    timerTextMeshPro.SetText(minutes.ToString("f0") + ":" + seconds.ToString("00"));
                }
                else//制限時間が経ったら
                {
                    if (!justOneResult)
                    {
                        justOneResult = true;
                        timerTextMeshPro.SetText(timeLimitString);
                        if (!isResult)
                        {
                            isResult = true;
                            StartCoroutine(NotEatRiceCake());
                        }
                    }
                }
            }
        }
    }
    /// <summary>
    /// クリア条件を設定[Inspector上で設定]
    /// </summary>
    /// <param name="condition">ScriptableObjectで設定したクリア条件</param>
    private void SetClearCondition(ClearConditions.ClearCondition.Conditions condition)
    {
        ClearConditions.ClearCondition conditionData = clearConditions.GetClearCondition(condition);

        if(conditionData != null)
        {
            clearConditionTagName = conditionData.needTagName;
            clearConditionTextString = conditionData.clearConditionTextString;
            clearConditionRiceCakeSize = conditionData.needRiceCakeSize;

            clearConditionTextMeshPro.text = clearConditionTextString;
            stageNum = conditionData.stageNum;
        }
        else
        {
            Debug.LogWarning($"クリア条件{condition}が見つかりません");
            clearConditionTagName = null;
            clearConditionTextString = null;
        }
    }
    /// <summary>
    /// リザルト時にオブジェクトを消して見やすくする
    /// </summary>
    private void DestroyObject()
    {
        hopping.SetActive(false);
        table.SetActive(false);
        foreach(GameObject destroyObject in otherObjectList)
        {
            destroyObject.SetActive(false);
        }
    }
    /// <summary>
    /// もぐもぐ
    /// </summary>
    /// <returns></returns>
    private void Eat()
    {
        isGamePlay = false;
        DestroyObject();
        resultCamera.Priority = resultCameraPriority;
        grondMomAndResultAnimator.SetTrigger(eatTrigger); //食べてるモーションに遷移
        SoundManager.Instance.PlaySE(SESource.eat);
    }
    //以下クリアかそうでないか
    /// <summary>
    /// おいしい(完璧)
    /// </summary>
    private IEnumerator VeryDeliciousRiceCake()
    {
        Eat();
        yield return new WaitForSeconds(waitTimeBeforeResult);
        SoundManager.Instance.PlaySE(SESource.veryDelicious);
        grondMomAndResultAnimator.SetTrigger(veryDeliciousTrigger);
        yield return new WaitForSeconds(delayBeforeTextVisible);
        SetTextVisible();
        yield break;
    }
    /// <summary>
    /// まあうまい(味付けが違う)
    /// </summary>
    private IEnumerator SoDeliciousRiceCake()
    {
        Eat();
        yield return new WaitForSeconds(waitTimeBeforeResult);
        SoundManager.Instance.PlaySE(SESource.soDelicious);
        grondMomAndResultAnimator.SetTrigger(soDeliciousTrigger);
        yield return new WaitForSeconds(delayBeforeTextVisible);
        SetTextVisible();
        yield break;
    }
    /// <summary>
    /// のどに餅が(餅の量が条件より多い)
    /// </summary>
    private IEnumerator TooMuchRiceCake()
    {
        Eat();
        yield return new WaitForSeconds(waitTimeBeforeResult);
        SoundManager.Instance.PlaySE(SESource.tooMuch);
        grondMomAndResultAnimator.SetTrigger(toomuchTrigger);
        yield return new WaitForSeconds(delayBeforeTextVisible);
        SetTextVisible();
        yield break;
    }
    /// <summary>
    /// 物足りない(餅の量が条件より少ない)
    /// </summary>
    private IEnumerator NotEnoughRiceCake()
    {
        Eat();
        yield return new WaitForSeconds(waitTimeBeforeResult);
        SoundManager.Instance.PlaySE(SESource.notEnough);
        grondMomAndResultAnimator.SetTrigger(notEnoughTrigger);
        yield return new WaitForSeconds(delayBeforeTextVisible);
        SetTextVisible();
        yield break;
    }
    /// <summary>
    /// なんか入った(Player自身が口の中に)
    /// </summary>
    private IEnumerator SomethingGotInThere()
    {
        Eat();
        yield return new WaitForSeconds(waitTimeBeforeResult);
        SoundManager.Instance.PlaySE(SESource.somethingGotIn);
        grondMomAndResultAnimator.SetTrigger(somethingGotItTrigger);
        yield return new WaitForSeconds(delayBeforeTextVisible);
        SetTextVisible();
        yield break;
    }
    /// <summary>
    /// 食えないじゃないか(時間切れ、もしくはPlayer自身がちゃぶ台の下に落ちた)
    /// </summary>
    public IEnumerator NotEatRiceCake()
    {
        isGamePlay = false;
        notEatResultCamera.Priority = resultCameraPriority;
        hopping.SetActive(false);
        foreach (GameObject destroyObject in otherObjectList)
        {
            destroyObject.SetActive(false);
        }
        yield return new WaitForSeconds(noteatWaitTime);
        grondMomAndResultAnimator.SetTrigger(noteatTrigger);
        SoundManager.Instance.PlaySE(SESource.notEat);
        yield return new WaitForSeconds(notEatAnimationDuration);
        yield return new WaitForSeconds(delayBeforeTextVisible);
        SetTextVisible();
        yield break;
    }
    /// <summary>
    /// もう一度やタイトルに戻るなどのボタンたちを表示
    /// </summary>
    private void SetTextVisible()
    {
        resultNextStageTMP.enabled = true;
        resultOneMoreTimeTMP.enabled = true;
        resultTitleTMP.enabled = true;
    }
    private MochiResultType EvaluateCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
            return MochiResultType.PlayerCollision;

        bool tagMatches = other.CompareTag(clearConditionTagName);
        int sizeComparison = riceCakeManager.RiceCakeSize.CompareTo(clearConditionRiceCakeSize);
        // -1: 小, 0: 等しい, 1: 大

        if (tagMatches)
        {
            switch (sizeComparison)
            {
                case 0: return MochiResultType.PerfectMatch;
                case 1: return MochiResultType.CorrectTagTooBig;
                case -1: return MochiResultType.CorrectTagTooSmall;
            }
        }
        else
        {
            switch (sizeComparison)
            {
                case 0: return MochiResultType.WrongTagSameSize;
                case 1: return MochiResultType.WrongTagTooBig;
                case -1: return MochiResultType.WrongTagTooSmall;
            }
        }

        return MochiResultType.PlayerCollision; // 念のため
    }
    /// <summary>
    /// このスクリプトはおばあちゃんの口の中にあり、衝突判定を取ることができる
    /// </summary>
    /// <param name="other">餅の状態によってクリア判定を変える</param>
    private void OnTriggerEnter(Collider other)
    {
        if (isResult) return;

        isResult = true;
        other.gameObject.SetActive(false);

        MochiResultType result = EvaluateCollision(other.gameObject);

        switch (result)
        {
            case MochiResultType.PerfectMatch:
                StartCoroutine(VeryDeliciousRiceCake());
                break;
            case MochiResultType.WrongTagSameSize:
                StartCoroutine(SoDeliciousRiceCake());
                break;
            case MochiResultType.WrongTagTooBig:
            case MochiResultType.CorrectTagTooBig:
                StartCoroutine(TooMuchRiceCake());
                break;
            case MochiResultType.WrongTagTooSmall:
            case MochiResultType.CorrectTagTooSmall:
                StartCoroutine(NotEnoughRiceCake());
                break;
            case MochiResultType.PlayerCollision:
                StartCoroutine(SomethingGotInThere());
                break;
        }
    } 
}
