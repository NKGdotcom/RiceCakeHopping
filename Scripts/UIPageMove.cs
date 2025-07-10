using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// タイトルUIでページを変える時の処理
/// </summary>
public class UIPageMove : MonoBehaviour
{
    [Header("ページに移るためのテキスト")]
    [SerializeField] protected TextMeshProUGUI movePageText;
    protected float colorChangeDuration = 0.1f;
    private string textColor = "FaceColor";
    private static readonly Color32 highlightColor = new Color32(255, 130, 130, 255);//テキストの上に置いたら少し赤っぽい色に
    private static readonly Color32 defaultColor = new Color32(255, 255, 255, 255);//テキストから外れた時
    //テキストをクリックした際に実行する関数
    protected UnityAction<BaseEventData> movePage;

    [Header("ページリスト")]
    [SerializeField] protected List<GameObject> uiPageList; //UIのページリスト
    [Header("ページを進めたり戻す矢印")]
    [SerializeField] protected GameObject rightArrow; //右矢印
    [SerializeField] protected GameObject leftArrow;  //左矢印
    [Header("ひとつ前に戻るテキスト")]
    [SerializeField] protected TextMeshProUGUI backPageText;
    private static readonly Color32 backTextDefaultColor = new Color32(0,0,0,255);

    private float arrowMoveSpeed = 25f; //矢印が上下に動く速さ

    protected int maxPageNum;  //ページ数
    protected int currentPage; //現在のページ

    private Vector3 initialArrowPos; //マウスから抜けたときに元の位置に戻す

    private Coroutine currentColorChangeCoroutine; //現在実行中の色が変わるアニメーションコルーチン
    private Coroutine currentArrowAnimation; // 現在実行中の矢印アニメーションコルーチン

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);

        maxPageNum = uiPageList.Count - 1; //リストは0から始まるため
        // ページをめくる矢印
        SetUpArrowEvent(rightArrow,
                        (eventData) => { currentArrowAnimation = StartCoroutine(ArrowEnter(rightArrow)); },
                        (eventData) => { ArrowExit(rightArrow); },
                        (eventData) => { TurnThePage(); ArrowExit(rightArrow);});
        //ページを戻す矢印
        SetUpArrowEvent(leftArrow,
                        (eventData) => { currentArrowAnimation = StartCoroutine(ArrowEnter(leftArrow)); },
                        (eventData) => { ArrowExit(leftArrow); },
                        (eventData) => { GoBackPage(); ArrowExit(leftArrow); });
        //ページを開くテキスト
        SetUpTextEvent(movePageText.gameObject,
                        (eventData) => { StartColorChange(movePageText, highlightColor); },
                        (eventData) => { StartColorChange(movePageText, defaultColor); },
                        (eventData) => {
                            movePage?.Invoke(eventData);
                            OpenPage();
                        });
        //ページを閉じるテキスト
        SetUpTextEvent(backPageText.gameObject,
                        (eventData) => { StartColorChange(backPageText, highlightColor); },
                        (eventData) => { StartColorChange(backPageText, backTextDefaultColor); },
                        (eventData) => { ClosePage(); });
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// ページに移るテキストの処理を設定
    /// </summary>
    /// <param name="_text">テキスト</param>
    /// <param name="_enterAction">テキストの上にマウスを置いたとき</param>
    /// <param name="_exitAction">テキストからマウスを離したとき</param>
    /// <param name="_clickAction">テキストをクリックしたら</param>
    protected void SetUpTextEvent(GameObject _text, UnityAction<BaseEventData> _enterAction, UnityAction<BaseEventData> _exitAction, UnityAction<BaseEventData> _clickAction)
    {
        AddEventTrigger(_text, EventTriggerType.PointerEnter, _enterAction);
        AddEventTrigger(_text, EventTriggerType.PointerExit, _exitAction);
        AddEventTrigger(_text, EventTriggerType.PointerClick, _clickAction);
    }
    /// <summary>
    /// テキストの色を徐々に変える処理
    /// </summary>
    /// <param name="_text">テキスト</param>
    /// <param name="_targetColor">どの色に変えるか</param>
    /// <param name="_duration">色が変わる時間</param>
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
    /// <summary>
    /// テキストの色を変える
    /// </summary>
    /// <param name="_text">テキスト</param>
    /// <param name="_changeColor">変更する色</param>
    protected void StartColorChange(TextMeshProUGUI _text, Color32 _changeColor)
    {
        currentColorChangeCoroutine = StartCoroutine(ChangeTextColorOverTime(_text, _changeColor, colorChangeDuration));
    }

    /// <summary>
    /// 矢印キーのイベントトリガーのセットアップ
    /// </summary>
    /// <param name="_arrow">矢印</param>
    /// <param name="_enterAction">マウスの上に置いたら起こる処理</param>
    /// <param name="_exitAction">マウスから離したら起こる処理</param>
    /// <param name="_clickAction">クリックしたら起こる処理</param>
    private void SetUpArrowEvent(GameObject _arrow, UnityAction<BaseEventData> _enterAction, UnityAction<BaseEventData> _exitAction, UnityAction<BaseEventData> _clickAction)
    {
        AddEventTrigger(_arrow, EventTriggerType.PointerEnter, _enterAction);
        AddEventTrigger(_arrow, EventTriggerType.PointerExit, _exitAction);
        AddEventTrigger(_arrow, EventTriggerType.PointerClick, _clickAction);
    }

    /// <summary>
    /// イベントトリガーの処理をつける
    /// </summary>
    /// <param name="_target">UI</param>
    /// <param name="_eventType">イベントトリガーの何を使うか</param>
    /// <param name="_action">実行したい関数</param>
    protected void AddEventTrigger(GameObject _target, EventTriggerType _eventType, UnityAction<BaseEventData> _action)
    {
        EventTrigger _trigger = _target.GetComponent<EventTrigger>() ?? _target.AddComponent<EventTrigger>();
        EventTrigger.Entry _entry = new EventTrigger.Entry { eventID = _eventType };
        _entry.callback.AddListener(_action);
        _trigger.triggers.Add(_entry);
    }

    /// <summary>
    /// マウスの上に入ったら起こる処理 (矢印の上下アニメーションを開始)
    /// </summary>
    /// <returns></returns>
    private IEnumerator ArrowEnter(GameObject _arrow)
    {
        initialArrowPos = _arrow.transform.localPosition;
        float _time = 0f;
        while (true)
        {
            float _offset = Mathf.Sin(_time * arrowMoveSpeed);
            _arrow.transform.localPosition = initialArrowPos + new Vector3(0, _offset, 0);
            _time += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// マウスから離れたら起こる処理 (矢印の上下アニメーションを停止し、元の位置に戻す)
    /// </summary>
    private void ArrowExit(GameObject _arrow)
    {
        if (currentArrowAnimation != null)
        {
            StopCoroutine(currentArrowAnimation);
            _arrow.transform.localPosition = initialArrowPos;
        }
    }

    /// <summary>
    /// ページをめくる
    /// </summary>
    protected void TurnThePage()
    {
        uiPageList[currentPage].SetActive(false);
        currentPage++;
        uiPageList[currentPage].SetActive(true);
        leftArrow.SetActive(true);

        if(currentPage == maxPageNum) rightArrow.SetActive(false); //最後のページの時
        else rightArrow.SetActive(true);

        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
    }

    /// <summary>
    /// ページを戻す
    /// </summary>
    protected void GoBackPage()
    {
        uiPageList[currentPage].SetActive(false);
        currentPage--;
        uiPageList[currentPage].SetActive(true);
        rightArrow.SetActive(true);

        if (currentPage == 0) leftArrow.SetActive(false); //最初のページの時
        else rightArrow.SetActive(true);

        SoundManager.Instance.PlaySE(SESource.backButton);
    }
    /// <summary>
    /// ページを開く
    /// </summary>
    protected void OpenPage()
    {
        if (currentPage != maxPageNum) rightArrow.SetActive(true);
        if (currentPage != 0) leftArrow.SetActive(true);

        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
    }
    /// <summary>
    /// ページを閉じる
    /// </summary>
    protected void ClosePage()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);

        TitleAnimationState.Instance.MoveToTitleNext();
        SoundManager.Instance.PlaySE(SESource.backButton);
    }
}