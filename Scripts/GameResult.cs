using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    public static GameResult Instance {  get; private set; }

    private SetClearConditions setClearConditions;
    [Header("���U���g���")]
    [SerializeField] private TextMeshProUGUI resultNextStageTMP;
    [SerializeField] private TextMeshProUGUI resultOneMoreTimeTMP;
    [SerializeField] private TextMeshProUGUI resultTitleTMP;
    [Header("���U���g���ɏ����I�u�W�F�N�g����")]
    [SerializeField] private GameObject hopping;
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject[] otherObjectList; //�e�[�u���̏�ɂ����Ă����Q���Ȃ�
    [Header("���΂������̃N���A�A�j���[�V����")]
    [SerializeField] private Animator grondMomAnimator;
    [Header("�V�l�}�V�[��")]
    [SerializeField] private CinemachineVirtualCamera resultCamera;
    [SerializeField] private CinemachineVirtualCamera notEatResultCamera;
    private int resultCameraPriority = 20;
    private int defaultCameraPriority = 10;

    private float riceCakeSize;

    private float waitTimeBeforeResult = 2.5f; //���͂�H�ׂĂ�Ԃ̑҂�����
    private float delayBeforeTextVisible = 0.5f; //��u�҂��ăe�L�X�g�\��
    private float notEatWaitTime = 0.2f; //�����҂��Ă��炿��ԑ�Ԃ��A�j���[�V����
    private float notEatAnimationDuration = 5 / 6; //�H���Ȃ�����Ȃ����Ƃ������㏭���҂�

    private bool isCorrectRiceCakeType; //�������݂̎�ނ�
    private bool isCorrectRiceCakeSize; //�K�؂ȃT�C�Y��
    private bool isSomethingEat;        //����������������
    private bool isBigRiceCake;         //�傫�����邩
    private bool isShortRiceCake;       //����Ȃ�������

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
    /// ���U���g���ɃI�u�W�F�N�g�������Č��₷������
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
    /// ��������
    /// </summary>
    /// <returns></returns>
    private void Eat()
    {
        GameStateMachine.Instance.SetState(GameStateMachine.GameState.Result);
        DestroyObjects();
        resultCamera.Priority = resultCameraPriority;
        grondMomAnimator.SetTrigger("Eat"); //�H�ׂĂ郂�[�V�����ɑJ��
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
        grondMomAnimator.SetTrigger("VeryDelicious");
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
        grondMomAnimator.SetTrigger("SoDelicious");
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
        grondMomAnimator.SetTrigger("TooMuch");
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
        grondMomAnimator.SetTrigger("NotEnough");
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
        SoundManager.Instance.PlaySE(SESource.notEnough);
        grondMomAnimator.SetTrigger("SomethingGotIt");
        yield return new WaitForSeconds(delayBeforeTextVisible);
        SetTextVisible();
        yield break;
    }
    /// <summary>
    /// �H���Ȃ�����Ȃ���(���Ԑ؂�A��������Player���g������ԑ�̉��ɗ�����)
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
    /// ������x��^�C�g���ɖ߂�Ȃǂ̃{�^��������\��
    /// </summary>
    private void SetTextVisible()
    {
        resultNextStageTMP.enabled = true;
        resultOneMoreTimeTMP.enabled = true;
        resultTitleTMP.enabled = true;
    }

    /// <summary>
    /// ���U���g�\��
    /// </summary>
    private void Result()
    {
        GameStateMachine.Instance.SetState(GameStateMachine.GameState.Result);
        if (isSomethingEat) StartCoroutine(SomethingGotInThere()); //�����������ꍇ
        else
        {
            if (isCorrectRiceCakeSize && isCorrectRiceCakeType) StartCoroutine(VeryDeliciousRiceCake());//��������
            else if (isBigRiceCake) StartCoroutine(TooMuchRiceCake()); //��������
            else if (isShortRiceCake) StartCoroutine(NotEnoughRiceCake()); //������Ƒ���Ȃ�
            else if (isCorrectRiceCakeSize && !isCorrectRiceCakeType) StartCoroutine(SoDeliciousRiceCake()); //�܂���������
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
