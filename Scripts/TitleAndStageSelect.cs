using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SoundManager;

public class TitleAndStageSelect : MonoBehaviour
{
    [Header("�X�e�[�W�I���Ƃ�������^�C�g���ɖ߂�{�^��")]
    [SerializeField] private GameObject stageSelectTextObj;
    [SerializeField] private TextMeshProUGUI stageSelectTMP;
    [SerializeField] private GameObject stageSelectBackTitleTextObj;
    [SerializeField] private TextMeshProUGUI stageSelectBackTitleTMP;

    [Header("�V�ѕ��Ƃ�������^�C�g���ɖ߂�{�^��")]
    [SerializeField] private GameObject howToPlayTextObj;
    [SerializeField] private TextMeshProUGUI howToPlayTMP;
    [SerializeField] private GameObject howToPlayBackTitleTextObj;
    [SerializeField] private TextMeshProUGUI howToPlayBackTitleTMP;
    private string textColor = "FaceColor";

	[Header("���")]
    [SerializeField] private GameObject rightYajirushi;
    [SerializeField] private GameObject leftYajirushi;
    private float yajirushiMoveSpeed = 5f;

    [Header("�y�[�W�ꗗ")]
    [SerializeField] private List<GameObject> stageSelectUIPageList;�@//�X�e�[�W�Z���N�g�y�[�W�ꗗ
    [SerializeField] private List<GameObject> howToPlayUIPageList; //�V�ѕ��y�[�W�ꗗ

   
    [System.Serializable]
    public class Stage
    {
        [SerializeField] private string sceneName;
        [SerializeField] private GameObject stageTextObj;
        [SerializeField] private TextMeshProUGUI stageTMP;
        public string SceneName => sceneName;
        public GameObject StageTextObj => stageTextObj;
        public TextMeshProUGUI StageTMP => stageTMP;
    }
	[Header("�X�e�[�W�ɑJ�ڂ���ۂ��߂̓o�^")]
	[SerializeField] private List<Stage> stageLists;

    [Header("�X�e�[�W�I���y�[�W�̍Ō�̃y�[�W�ԍ�")]
    [SerializeField] public int finalStageSelectPage;//�X�e�[�W�I���̍Ō�̃y�[�W�ԍ�
    private int stageSelectPage;
	[Header("�V�ѕ��y�[�W�̍Ō�̃y�[�W�ԍ�")]
	[SerializeField] public int finalHowToPlayPage;//�V�ѕ��̍Ō�̃y�[�W�ԍ�
    private int howToPlayPage;
    private int firstPage = 1;
    private int nextPageOne = 1;
    private int backPageOne = 1;
        
    //�F��ς���
    private static readonly Color32 highlightColor = new Color32(255, 130, 130, 255); //�e�L�X�g�Ƀ}�E�X��u�����Ƃ�
    private static readonly Color32 defaultColor = new Color32(0, 0, 0, 255);�@//�e�L�X�g����}�E�X�𗣂�����
    private static readonly Color32 whiteColor = new Color(255, 255, 255, 255);
    private const float ColorChangeDuration = 0.1f;

    private bool isStageSelect;//�X�e�[�W�I�𒆂��ǂ���
    private bool isHowToPlay;//�V�ѕ���ʂ��ǂ���

    [Header("Animator�֘A")]
    [SerializeField] private Animator titleAnimator;
    private string tapAnyKeyBool = "TapAnyKey";
    private string gameStartBool = "GameStart";
    private string gotoStageSelectBool = "GoToStageSelect";
    private string gotoHowToPlayBool = "GoToHowToPlay";

    private Dictionary<TextMeshProUGUI, Coroutine> colorCoroutines = new Dictionary<TextMeshProUGUI, Coroutine>();
    private Dictionary<string, AudioClip> soundDictionary;

    private Coroutine rightArrowMoveCoroutine;
    private Coroutine leftArrowMoveCoroutine;

    private float fadeOutWaitTime = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        InitializeSoundDictionary();

        SoundManager.Instance.PlayBGM(BGMSource.title);

        stageSelectPage = firstPage;
        howToPlayPage = firstPage;
        isStageSelect = false;
        isHowToPlay = false;

        //�X�e�[�W�I����ʂɐi��
        SetupEvents(stageSelectTextObj.gameObject, (x) => StageSelectTMPMouseEnter(), (x) => StageSelectTMPMouseExit(), (x) => StageSelectTMPMouseClick());
        //�V�ѕ���ʂɐi��
        SetupEvents(howToPlayTextObj, (x) =>HowToPlayTMPMouseEnter(), (x) => HowToPlayTMPMouseExit(), (x) => HowToPlayTMPMouseClick());
        //���{�^���̎d�g��
        SetupEvents(rightYajirushi, (x) => RightYajirushiButtonMouseEnter(), (x) => RightYajirushiButtonMouseExit(), (x) => RightYajirushiButtonMouseClick());
        SetupEvents(leftYajirushi, (x) => LeftYajirushiButtonMouseEnter(), (x) => LeftYajirushiButtonMouseExit(), (x) => LeftYajirushiButtonMouseClick());
        //�^�C�g���֖߂�
        SetupEvents(stageSelectBackTitleTextObj, (x) => StageSelectBackTitleMouseEnter(), (x) => StageSelectBackTitleMouseExit(), (x) => StageSelectBackTitleMouseClick());
        SetupEvents(howToPlayBackTitleTextObj, (x) => HowToPlayBackTitleMouseEnter(), (x) => HowToPlayBackTitleMouseExit(), (x) => HowToPlayBackTitleMouseClick());

        //�Ώ̂̃X�e�[�W�ɐi��
        foreach (Stage stage in stageLists)
        {
            SetupEvents(stage.StageTextObj, (x) => SetStageEnter(stage), (x) => SetStageExit(stage), (x) => StartCoroutine(SetStageClick(stage)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Print))
        {
            titleAnimator.SetBool(tapAnyKeyBool, true);
        }
    }
    private void AddEventTrigger(GameObject _target, EventTriggerType _eventType, UnityAction<BaseEventData> _action)
    {
        EventTrigger trigger = _target.GetComponent<EventTrigger>() ?? _target.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = _eventType };
        entry.callback.AddListener(_action);
        trigger.triggers.Add(entry);
    }
    private void SetupEvents(GameObject _target, UnityAction<BaseEventData> _enterAction, UnityAction<BaseEventData> _exitAction, UnityAction<BaseEventData> _clickAction)
    {
        AddEventTrigger(_target, EventTriggerType.PointerEnter, _enterAction);
        AddEventTrigger(_target, EventTriggerType.PointerExit, _exitAction);
        AddEventTrigger(_target, EventTriggerType.PointerClick, _clickAction);
    }
    /// <summary>
    /// �F���ς��
    /// </summary>
    /// <param name="text"></param>
    /// <param name="targetColor">�ǂ̐F�ɂ��邩</param>
    /// <param name="duration">���b������</param>
    /// <returns></returns>
    private IEnumerator ChangeTextColorOverTime(TextMeshProUGUI _text, Color32 _targetColor, float _duration)
    {
        Color32 startColor = _text.color;
        float _elapsed = 0f;

        while (_elapsed < _duration)
        {
            _elapsed += Time.deltaTime;
            float _t = _elapsed / _duration;
            _text.color = Color.Lerp(startColor, _targetColor, _t);
            _text.fontMaterial.SetColor(textColor, _text.color); // �}�e���A���̐F�����
            yield return null;
        }

        _text.color = _targetColor; // �ŏI�F�ɐݒ�
        _text.fontMaterial.SetColor(textColor, _targetColor);
    }
    private void StartColorChange(TextMeshProUGUI _text, Color32 _targetColor)
    {
        if (colorCoroutines.TryGetValue(_text, out Coroutine _coroutine))
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
        }
        Coroutine _newCoroutine = StartCoroutine(ChangeTextColorOverTime(_text, _targetColor, ColorChangeDuration));
        colorCoroutines[_text] = _newCoroutine;
    }
    /// <summary>
    /// �����㉺�ɐU��������
    /// </summary>
    private IEnumerator MoveArrowUpDown(GameObject _arrow)
    {
        Vector3 _startPosition = _arrow.transform.localPosition;
        float _time = 0f;
        while (true)
        {
            float offset = Mathf.Sin(_time * yajirushiMoveSpeed) * yajirushiMoveSpeed;
            _arrow.transform.localPosition = _startPosition + new Vector3(0, offset, 0);
            _time += Time.deltaTime;
            yield return null;
        }
    }

    //�ȉ��̓{�^������
    //�X�e�[�W�̃{�^��
    private void SetStageEnter(Stage _stage)
    {
        StartColorChange(_stage.StageTMP, highlightColor);
    }
    private void SetStageExit(Stage _stage)
    {
        StartColorChange(_stage.StageTMP, defaultColor);
    }
    private IEnumerator SetStageClick(Stage _stage)
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);

        titleAnimator.SetBool(gameStartBool,true);
        // �����҂��Ă���V�[���J�ځi��: �t�F�[�h�A�E�g�҂��Ȃǁj
        yield return new WaitForSeconds(fadeOutWaitTime);

        SceneManager.LoadScene(_stage.SceneName);
    }
    //�X�e�[�W�I����ʂ̃^�C�g���֖߂�
    private void StageSelectBackTitleMouseEnter()
    {
        StartColorChange(stageSelectBackTitleTMP, highlightColor);
    }
    private void StageSelectBackTitleMouseExit()
    {
        StartColorChange(stageSelectBackTitleTMP, defaultColor);
    }
    private void StageSelectBackTitleMouseClick()
    {
        SoundManager.Instance.PlaySE(SESource.backButton);
        isStageSelect = false;
        rightYajirushi.SetActive(false);
        leftYajirushi.SetActive(false);
        titleAnimator.SetBool(gotoStageSelectBool, false);
    }
    //�X�e�[�W�ꗗ�{�^��
    private void StageSelectTMPMouseEnter()
    {
        StartColorChange(stageSelectTMP, highlightColor);
    }
    private void StageSelectTMPMouseExit()
    {
        StartColorChange(stageSelectTMP, whiteColor);
    }
    private void StageSelectTMPMouseClick()
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        isStageSelect = true;
        rightYajirushi.SetActive(true);
        leftYajirushi.SetActive(true);
        titleAnimator.SetBool(gotoStageSelectBool, true);
        if(finalStageSelectPage == firstPage)
        {
            rightYajirushi.SetActive(false);
            leftYajirushi.SetActive(false);
        }
        if(stageSelectPage == firstPage)
        {
            leftYajirushi.SetActive(false);
        }
        else if (stageSelectPage == finalStageSelectPage)
        {
            rightYajirushi.SetActive(false);
        }
    }
    //������@�̃^�C�g���֖߂�
    private void HowToPlayBackTitleMouseEnter()
    {
        StartColorChange(howToPlayBackTitleTMP, highlightColor);
    }
    private void HowToPlayBackTitleMouseExit()
    {
        StartColorChange(howToPlayBackTitleTMP, defaultColor);
    }
    private void HowToPlayBackTitleMouseClick()
    {
        SoundManager.Instance.PlaySE(SESource.backButton);
        isHowToPlay = false;
        rightYajirushi.SetActive(false);
        leftYajirushi.SetActive(false);
        titleAnimator.SetBool(gotoHowToPlayBool, false);
    }
    //������@�{�^��
    private void HowToPlayTMPMouseEnter()
    {
        StartColorChange(howToPlayTMP, highlightColor);
    }
    private void HowToPlayTMPMouseExit()
    {
        StartColorChange(howToPlayTMP, whiteColor);
    }
    private void HowToPlayTMPMouseClick()
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        isHowToPlay = true;
        rightYajirushi.SetActive(true);
        leftYajirushi.SetActive(true);
        titleAnimator.SetBool(gotoHowToPlayBool, true);
        if (finalHowToPlayPage == firstPage)
        {
            rightYajirushi.SetActive(false);
            
        }
        if (howToPlayPage == firstPage)
        {
            leftYajirushi.SetActive(false);
        }
        else if(howToPlayPage == finalHowToPlayPage)
        {
            rightYajirushi.SetActive(false);
        }
    }

    //���{�^��

    private void HandleArrowButtonClick(bool _isRightArrow)
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);

        if (isStageSelect) 
        {
            HandlePageChange(_isRightArrow, ref stageSelectPage,stageSelectUIPageList,finalStageSelectPage);
        }
        else if (isHowToPlay)
        {
            HandlePageChange(_isRightArrow, ref howToPlayPage, howToPlayUIPageList, finalHowToPlayPage);
        }
    }
    private void HandlePageChange(bool _isNext, ref int _currentPage, List<GameObject> _uiList, int _finalPage)
    {
        int newPage = _isNext ? _currentPage + nextPageOne : _currentPage - backPageOne;

        if (newPage >= firstPage && newPage <= _finalPage)
        {
            _uiList[_currentPage - backPageOne].SetActive(false);
            _currentPage = newPage;
            _uiList[_currentPage - backPageOne].SetActive(true);
        }

        rightYajirushi.SetActive(_currentPage != _finalPage);
        leftYajirushi.SetActive(_currentPage != firstPage);
    }
    private void RightYajirushiButtonMouseEnter()
    {
        if (rightArrowMoveCoroutine != null)
        {
            StopCoroutine(rightArrowMoveCoroutine);
        }
        rightArrowMoveCoroutine = StartCoroutine(MoveArrowUpDown(rightYajirushi));
    }
    private void RightYajirushiButtonMouseExit()
    {
        if (rightArrowMoveCoroutine != null)
        {
            StopCoroutine(rightArrowMoveCoroutine);
            rightYajirushi.transform.localPosition = new Vector3(rightYajirushi.transform.localPosition.x, 0, rightYajirushi.transform.localPosition.z);
        }
    }
    private void RightYajirushiButtonMouseClick()
    {
        HandleArrowButtonClick(true);
    }
    private void LeftYajirushiButtonMouseEnter()
    {
        if (leftArrowMoveCoroutine != null)
        {
            StopCoroutine(leftArrowMoveCoroutine);
        }
        leftArrowMoveCoroutine = StartCoroutine(MoveArrowUpDown(leftYajirushi));
    }
    private void LeftYajirushiButtonMouseExit()
    {
        if (leftArrowMoveCoroutine != null)
        {
            StopCoroutine(leftArrowMoveCoroutine);
            leftYajirushi.transform.localPosition = new Vector3(leftYajirushi.transform.localPosition.x, 0, leftYajirushi.transform.localPosition.z);
        }
    }
    private void LeftYajirushiButtonMouseClick()
    {
        HandleArrowButtonClick(false);
    }
    private void InitializeSoundDictionary()
    {
        soundDictionary = new Dictionary<string, AudioClip>();
    }
}
