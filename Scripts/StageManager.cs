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

    [Header("���̎Q�ƃN���X")]
    [SerializeField] private ClearConditions clearConditions;
    [SerializeField] private RiceCakeManager riceCakeManager;

    [Header("�V�[������UI�֘A")]
    [SerializeField] private TextMeshProUGUI timerTextMeshPro;
    [SerializeField] private TextMeshProUGUI clearConditionTextMeshPro; //�N���A�����������e�L�X�g
    
    [Header("���U���g����UI�֘A")]
    [SerializeField] private TextMeshProUGUI resultNextStageTMP;
    [SerializeField] private TextMeshProUGUI resultOneMoreTimeTMP;
    [SerializeField] private TextMeshProUGUI resultTitleTMP;

    [Header("�������Ԃ⎞�Ԋ֘A")]
    [SerializeField] private float timeLimit = 60; //��������
    private float timeLimitBoundary = 0;
    private string timeLimitString = "0:00";
    [SerializeField] private float waitTime = 2.5f; //�N���A������������܂ő���ł��Ȃ�����
    private float waitTimeBoundary = 0;
    private const float oneMinute = 60;
    private float minutes;
    private float seconds;

    [Header("���U���g���ɏ����I�u�W�F�N�g����")]
    [SerializeField] private GameObject hopping;
    [SerializeField] private GameObject table;
    [SerializeField] private List<GameObject> otherObjectList; //����ȊO

    [Header("�J�����ړ����s���ϐ�")]
    //�J�����ړ����s��
    [SerializeField] private CinemachineVirtualCamera resultCamera;
    [SerializeField] private CinemachineVirtualCamera notEatResultCamera;
    private int resultCameraPriority = 20;
    private int defaultCameraPriority = 10;

    [Header("���΂������̃N���A���̃A�j���[�^�[")]
    [SerializeField] private Animator grondMomAndResultAnimator;
    //animation�̃g���K�[
    private string eatTrigger = "Eat";
    private string noteatTrigger = "NotEat";
    private string veryDeliciousTrigger = "VeryDelicious";
    private string soDeliciousTrigger = "SoDelicious";
    private string toomuchTrigger = "TooMuch";
    private string notEnoughTrigger = "NotEnough";
    private string somethingGotItTrigger = "SomethingGotIt";

    [Header("�N���A����")]
    [SerializeField] private ClearConditions.ClearCondition.Conditions conditionType;
    private string clearConditionTagName;
    private string clearConditionTextString;
    private float clearConditionRiceCakeSize;

    private float waitTimeBeforeResult = 2.5f;
    private float delayBeforeTextVisible = 0.5f;
    private float notEatAnimationDuration = 5 / 6f;
    private float noteatWaitTime = 0.2f;

    private bool isGamePlay = false; //�v���C����
    private bool isPause = false; //�|�[�Y����
    private bool isResult = false; //���U���g��
    private bool justOneResult = false; //SE���肷���Ȃ��悤��

    private int stageNum;
    /// <summary>
    /// �݂̃��U���g
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
        SetClearCondition(conditionType);//�N���A�������Q��

        SoundManager.Instance.PlayBGM(BGMSource.stageBGM);

        isGamePlay = false;
        isPause = false;
        isResult = false;
        justOneResult = false;

        Debug.Log("timeLimit: " + timeLimit);
        Debug.Log("timeLimitBoundary: " + timeLimitBoundary);
        seconds = Mathf.Floor(Mathf.Repeat(timeLimit, oneMinute));
        minutes = Mathf.Floor((timeLimit) / oneMinute);
        timerTextMeshPro.SetText(minutes.ToString("f0") + ":" + seconds.ToString("00"));//�������Ԃ�ݒ�
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePlay)//���߂̃N���A��������������
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
        else//�Q�[����
        {
            if (!isPause)//�|�[�Y��ʂɍs���Ă��Ȃ���
            {
                timeLimit -= Time.deltaTime;
                if (timeLimit > timeLimitBoundary)
                {
                    seconds = Mathf.Floor(Mathf.Repeat(timeLimit + 1, oneMinute));
                    minutes = Mathf.Floor((timeLimit + 1) / oneMinute);
                    timerTextMeshPro.SetText(minutes.ToString("f0") + ":" + seconds.ToString("00"));
                }
                else//�������Ԃ��o������
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
    /// �N���A������ݒ�[Inspector��Őݒ�]
    /// </summary>
    /// <param name="condition">ScriptableObject�Őݒ肵���N���A����</param>
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
            Debug.LogWarning($"�N���A����{condition}��������܂���");
            clearConditionTagName = null;
            clearConditionTextString = null;
        }
    }
    /// <summary>
    /// ���U���g���ɃI�u�W�F�N�g�������Č��₷������
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
    /// ��������
    /// </summary>
    /// <returns></returns>
    private void Eat()
    {
        isGamePlay = false;
        DestroyObject();
        resultCamera.Priority = resultCameraPriority;
        grondMomAndResultAnimator.SetTrigger(eatTrigger); //�H�ׂĂ郂�[�V�����ɑJ��
        SoundManager.Instance.PlaySE(SESource.eat);
    }
    //�ȉ��N���A�������łȂ���
    /// <summary>
    /// ��������(����)
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
    /// �܂����܂�(���t�����Ⴄ)
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
    /// �̂ǂɖ݂�(�݂̗ʂ�������葽��)
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
    /// ������Ȃ�(�݂̗ʂ�������菭�Ȃ�)
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
    /// �Ȃ񂩓�����(Player���g�����̒���)
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
    /// �H���Ȃ�����Ȃ���(���Ԑ؂�A��������Player���g������ԑ�̉��ɗ�����)
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
    /// ������x��^�C�g���ɖ߂�Ȃǂ̃{�^��������\��
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
        // -1: ��, 0: ������, 1: ��

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

        return MochiResultType.PlayerCollision; // �O�̂���
    }
    /// <summary>
    /// ���̃X�N���v�g�͂��΂������̌��̒��ɂ���A�Փ˔������邱�Ƃ��ł���
    /// </summary>
    /// <param name="other">�݂̏�Ԃɂ���ăN���A�����ς���</param>
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
