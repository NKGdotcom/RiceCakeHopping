using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ選択nのページと遊び方のページをそれぞれ統括するクラス
/// </summary>
public class PageController : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("次のページに進むボタン")]
    [SerializeField] private RightButtonArrowContoroller rightButtonArrowController;
    [Tooltip("前のページに戻るボタン")]
    [SerializeField] private LeftButtonArrowController leftButtonArrowController;
    [Tooltip("ページを切り替える")]
    [SerializeField] private ChangePage changePage;
    [Tooltip("タイトルに戻るテキストボタン")]
    [SerializeField] private BackTitleTextController backTitleTextController;

    void Awake()
    {
        if (rightButtonArrowController == null) { Debug.LogError("rightButtonArrowControllerが参照されていません。"); }
        if (leftButtonArrowController == null) { Debug.LogError("leftButtonArrowControllerが参照されていません。"); return;}
        if (changePage == null) { Debug.LogError("changePageが参照されていません。"); return; }
        if (backTitleTextController == null) { Debug.LogError("backTitleTextController"); return; }

        rightButtonArrowController.OnClicked += ProceedPage;
        leftButtonArrowController.OnClicked += BackPage;
        changePage.OnLastPage += ReachLastPage;
        changePage.OnFirstPage += ReachFirstPage;
        backTitleTextController.OnClicked += ClosePage;
    }

    /// <summary>
    /// 開いたページのセットアップ
    /// </summary>
    public void SetPage()
    {
        rightButtonArrowController.gameObject.SetActive(true);
        leftButtonArrowController.gameObject.SetActive(true);
        changePage.SetupPage();
    }

    /// <summary>
    /// ページを進める
    /// </summary>
    public void ProceedPage()
    {
        leftButtonArrowController.gameObject.SetActive(true);
        rightButtonArrowController.gameObject.SetActive(true);
        changePage.AdvancePage();
    }

    /// <summary>
    /// ページを戻す
    /// </summary>
    public void BackPage()
    {
        leftButtonArrowController.gameObject.SetActive(true);
        rightButtonArrowController.gameObject.SetActive(true);
        changePage.ReturnPage();
    }

    /// <summary>
    /// 最後のページに到達した
    /// </summary>
    public void ReachLastPage()
    {
        rightButtonArrowController.gameObject.SetActive(false);
    }

    /// <summary>
    /// 初めのページに到達した
    /// </summary>
    public void ReachFirstPage()
    {
        leftButtonArrowController.gameObject.SetActive(false);
    }

    /// <summary>
    /// ページを戻す
    /// </summary>
    public void ClosePage()
    {
        changePage.ClosePage();
    }
}
