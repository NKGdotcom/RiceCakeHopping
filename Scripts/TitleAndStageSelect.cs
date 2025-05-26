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
    [Header("ステージ選択とそこからタイトルに戻るボタン")]
    [SerializeField] private GameObject stageSelectTextObj;
    [SerializeField] private TextMeshProUGUI stageSelectTMP;
    [SerializeField] private GameObject stageSelectBackTitleTextObj;
    [SerializeField] private TextMeshProUGUI stageSelectBackTitleTMP;

    [Header("遊び方とそこからタイトルに戻るボタン")]
    [SerializeField] private GameObject howToPlayTextObj;
    [SerializeField] private TextMeshProUGUI howToPlayTMP;
    [SerializeField] private GameObject howToPlayBackTitleTextObj;
    [SerializeField] private TextMeshProUGUI howToPlayBackTitleTMP;
    private string textColor = "FaceColor";

	[Header("矢印")]
    [SerializeField] private GameObject rightYajirushi;
    [SerializeField] private GameObject leftYajirushi;
    private float yajirushiMoveSpeed = 5f;

    [Header("ページ一覧")]
    [SerializeField] private List<GameObject> stageSelectUIPageList;　//ステージセレクトページ一覧
    [SerializeField] private List<GameObject> howToPlayUIPageList; //遊び方ページ一覧

   
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
	[Header("ステージに遷移する際ための登録")]
	[SerializeField] private List<Stage> stageLists;

    [Header("ステージ選択ページの最後のページ番号")]
    [SerializeField] public int finalStageSelectPage;//ステージ選択の最後のページ番号
    private int stageSelectPage;
	[Header("遊び方ページの最後のページ番号")]
	[SerializeField] public int finalHowToPlayPage;//遊び方の最後のページ番号
    private int howToPlayPage;
    private int firstPage = 1;
    private int nextPageOne = 1;
    private int backPageOne = 1;
        
    //色を変える
    private static readonly Color32 highlightColor = new Color32(255, 130, 130, 255); //テキストにマウスを置いたとき
    private static readonly Color32 defaultColor = new Color32(0, 0, 0, 255);　//テキストからマウスを離した時
    private static readonly Color32 whiteColor = new Color(255, 255, 255, 255);
    private const float ColorChangeDuration = 0.1f;

    private bool isStageSelect;//ステージ選択中かどうか
    private bool isHowToPlay;//遊び方画面かどうか

    [Header("Animator関連")]
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

        //ステージ選択画面に進む
        SetupEvents(stageSelectTextObj.gameObject, (x) => StageSelectTMPMouseEnter(), (x) => StageSelectTMPMouseExit(), (x) => StageSelectTMPMouseClick());
        //遊び方画面に進む
        SetupEvents(howToPlayTextObj, (x) =>HowToPlayTMPMouseEnter(), (x) => HowToPlayTMPMouseExit(), (x) => HowToPlayTMPMouseClick());
        //矢印ボタンの仕組み
        SetupEvents(rightYajirushi, (x) => RightYajirushiButtonMouseEnter(), (x) => RightYajirushiButtonMouseExit(), (x) => RightYajirushiButtonMouseClick());
        SetupEvents(leftYajirushi, (x) => LeftYajirushiButtonMouseEnter(), (x) => LeftYajirushiButtonMouseExit(), (x) => LeftYajirushiButtonMouseClick());
        //タイトルへ戻る
        SetupEvents(stageSelectBackTitleTextObj, (x) => StageSelectBackTitleMouseEnter(), (x) => StageSelectBackTitleMouseExit(), (x) => StageSelectBackTitleMouseClick());
        SetupEvents(howToPlayBackTitleTextObj, (x) => HowToPlayBackTitleMouseEnter(), (x) => HowToPlayBackTitleMouseExit(), (x) => HowToPlayBackTitleMouseClick());

        //対称のステージに進む
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
    /// 色が変わる
    /// </summary>
    /// <param name="text"></param>
    /// <param name="targetColor">どの色にするか</param>
    /// <param name="duration">何秒かけて</param>
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
            _text.fontMaterial.SetColor(textColor, _text.color); // マテリアルの色も補間
            yield return null;
        }

        _text.color = _targetColor; // 最終色に設定
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
    /// 矢印を上下に振動させる
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

    //以下はボタン処理
    //ステージのボタン
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
        // 少し待ってからシーン遷移（例: フェードアウト待ちなど）
        yield return new WaitForSeconds(fadeOutWaitTime);

        SceneManager.LoadScene(_stage.SceneName);
    }
    //ステージ選択画面のタイトルへ戻る
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
    //ステージ一覧ボタン
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
    //操作方法のタイトルへ戻る
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
    //操作方法ボタン
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

    //矢印ボタン

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
