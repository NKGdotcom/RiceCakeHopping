using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// タイトルUIでページを変える時の処理
/// </summary>
public class UIPageMove : TextColorChange
{
    [Header("ページに移るためのテキスト")]
    [SerializeField] protected TextMeshProUGUI movePageText;

    //テキストをクリックした際に実行する関数
    protected UnityAction<BaseEventData> movePage;

    [Header("ページリスト")]
    [SerializeField] protected List<GameObject> uiPageList; //UIのページリスト
    [Header("ページを進めたり戻す矢印")]
    [SerializeField] protected GameObject rightArrow; //右矢印
    [SerializeField] protected GameObject leftArrow;  //左矢印
    [Header("ひとつ前に戻るテキスト")]
    [SerializeField] protected TextMeshProUGUI backPageText;

    private float arrowMoveSpeed = 25f; //矢印が上下に動く速さ

    protected int maxPageNum;  //ページ数
    protected int currentPage; //現在のページ

    private Vector3 initialArrowPos; //マウスから抜けたときに元の位置に戻す

    private Coroutine currentArrowAnimation; // 現在実行中の矢印アニメーションコルーチン

    // Start is called before the first frame update
    protected override void Start()
    {

        rightArrow.SetActive(false);
        leftArrow.SetActive(false);

        maxPageNum = uiPageList.Count - 1; //リストは0から始まるため
        // ページをめくる矢印
        SetUpArrowEvent(rightArrow,
                        (eventData) => { currentArrowAnimation = StartCoroutine(ArrowEnter(rightArrow)); },
                        (eventData) => { ArrowExit(rightArrow); },
                        (eventData) => { TurnThePage(); ArrowExit(rightArrow); });
        //ページを戻す矢印
        SetUpArrowEvent(leftArrow,
                        (eventData) => { currentArrowAnimation = StartCoroutine(ArrowEnter(leftArrow)); },
                        (eventData) => { ArrowExit(leftArrow); },
                        (eventData) => { GoBackPage(); ArrowExit(leftArrow); });
        //ページを開くテキスト
        SetUpTextEvent(movePageText.gameObject,
                        (eventData) => { ChangeWhiteText(movePageText); },
                        (eventData) => { ResetWhiteText(movePageText); },
                        (eventData) => {
                            movePage?.Invoke(eventData);
                            OpenPage();
                        });
        //ページを閉じるテキスト
        SetUpTextEvent(backPageText.gameObject,
                        (eventData) => { ChangeBlackText(backPageText); },
                        (eventData) => { ResetBlackText(backPageText); },
                        (eventData) => { ClosePage(); });
    }

    // Update is called once for frame
    void Update()
    {

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

        if (currentPage == maxPageNum) rightArrow.SetActive(false); //最後のページの時
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