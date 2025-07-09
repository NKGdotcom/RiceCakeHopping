using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    public static GameResult Instance {  get; private set; }

    private SetClearConditions setClearConditions;
    [Header("リザルト画面")]
    [SerializeField] private TextMeshProUGUI resultNextStageTMP;
    [SerializeField] private TextMeshProUGUI resultOneMoreTimeTMP;
    [SerializeField] private TextMeshProUGUI resultTitleTMP;
    [Header("リザルト時に消すオブジェクトたち")]
    [SerializeField] private GameObject hopping;
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject[] otherObjectList; //テーブルの上においてある障害物など
    [Header("おばあちゃんのクリアアニメーション")]
    [SerializeField] private Animator grondMomAnimator;
    [Header("シネマシーン")]
    [SerializeField] private CinemachineVirtualCamera resultCamera;
    [SerializeField] private CinemachineVirtualCamera notEatResultCamera;
    private int resultCameraPriority = 20;
    private int defaultCameraPriority = 10;

    private float riceCakeSize;

    private float waitTimeBeforeResult = 2.5f; //ごはん食べてる間の待ち時間
    private float delayBeforeTextVisible = 0.5f; //一瞬待ってテキスト表示
    private float notEatWaitTime = 0.2f; //少し待ってからちゃぶ台返しアニメーション
    private float notEatAnimationDuration = 5 / 6; //食えないじゃないかといった後少し待つ

    private bool isCorrectRiceCakeType; //正しい餅の種類か
    private bool isCorrectRiceCakeSize; //適切なサイズか
    private bool isSomethingEat;        //何か物が入ったか
    private bool isBigRiceCake;         //大きすぎるか
    private bool isShortRiceCake;       //足りなさすぎる

    public TextMeshProUGUI ResultNextSceneTMP { get => resultNextStageTMP; private set => resultNextStageTMP = value; }
    public TextMeshProUGUI ResultOneMoreTimeTMP { get => resultOneMoreTimeTMP; private set => resultOneMoreTimeTMP = value; }
    public TextMeshProUGUI ResultTitleTMP { get => resultTitleTMP; private set => resultTitleTMP = value; }
    public Animator GroundMomAnmator { get => grondMomAnimator; private set => grondMomAnimator = value; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        setClearConditions = GetComponent<SetClearConditions>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// リザルト時にオブジェクトを消して見やすくする
    /// </summary>
    private void DestroyObjects()
    {
        hopping.SetActive(false);
        table.SetActive(false);
        foreach (GameObject _destroyObject in otherObjectList)
        {
            _destroyObject.SetActive(false);
        }
    }
    /// <summary>
    /// もぐもぐ
    /// </summary>
    /// <returns></returns>
    private void Eat()
    {
        GameStateMachine.Instance.SetState(GameStateMachine.GameState.Result);
        DestroyObjects();
        resultCamera.Priority = resultCameraPriority;
        grondMomAnimator.SetTrigger("Eat"); //食べてるモーションに遷移
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
        grondMomAnimator.SetTrigger("VeryDelicious");
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
        grondMomAnimator.SetTrigger("SoDelicious");
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
        grondMomAnimator.SetTrigger("TooMuch");
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
        grondMomAnimator.SetTrigger("NotEnough");
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
        SoundManager.Instance.PlaySE(SESource.notEnough);
        grondMomAnimator.SetTrigger("SomethingGotIt");
        yield return new WaitForSeconds(delayBeforeTextVisible);
        SetTextVisible();
        yield break;
    }
    /// <summary>
    /// 食えないじゃないか(時間切れ、もしくはPlayer自身がちゃぶ台の下に落ちた)
    /// </summary>
    public IEnumerator NotEatRiceCake()
    {
        GameStateMachine.Instance.SetState(GameStateMachine.GameState.Result);
        hopping.SetActive(false);
        notEatResultCamera.Priority = resultCameraPriority;
        foreach (GameObject destroyObject in otherObjectList)
        {
            destroyObject.SetActive(false);
        }
        yield return new WaitForSeconds(notEatWaitTime);
        grondMomAnimator.SetTrigger("NotEat");
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

    /// <summary>
    /// リザルト表示
    /// </summary>
    private void Result()
    {
        GameStateMachine.Instance.SetState(GameStateMachine.GameState.Result);
        if (isSomethingEat) StartCoroutine(SomethingGotInThere()); //何か入った場合
        else
        {
            if (isCorrectRiceCakeSize && isCorrectRiceCakeType) StartCoroutine(VeryDeliciousRiceCake());//おいしい
            else if (isBigRiceCake) StartCoroutine(TooMuchRiceCake()); //多すぎる
            else if (isShortRiceCake) StartCoroutine(NotEnoughRiceCake()); //ちょっと足りない
            else if (isCorrectRiceCakeSize && !isCorrectRiceCakeType) StartCoroutine(SoDeliciousRiceCake()); //まあおいしい
        }
    }
    private void OnTriggerEnter(Collider _other)
    {
        if(_other.gameObject.CompareTag("Normal")|| _other.gameObject.CompareTag("KinakoRiceCake") || _other.gameObject.CompareTag("SoySourceRiceCake"))
        {
            _other.gameObject.SetActive(false);
            RiceCakeManager _riceCakeManager = GetComponent<RiceCakeManager>();
            riceCakeSize = _riceCakeManager.RiceCakeSize;
            _riceCakeManager.GameObjectFalse();
        }
        if (_other.gameObject.CompareTag(setClearConditions.RiceCakeTag)) isCorrectRiceCakeType = true;
        else if (_other.gameObject.CompareTag("Player")) isSomethingEat = true;

        if (setClearConditions.RiceCakeSize == riceCakeSize) isCorrectRiceCakeSize = true;
        else if (setClearConditions.RiceCakeSize < riceCakeSize) isBigRiceCake = true;
        else if (setClearConditions.RiceCakeSize > riceCakeSize) isShortRiceCake = true;

        Result();
    }
}
