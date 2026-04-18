using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ページを切り替えるクラス
/// </summary>
public class ChangePage : MonoBehaviour
{
    [Header("ページ管理")]
    [Tooltip("切り替えるページの配列")]
    [SerializeField] private GameObject[] pages;
    private int nowPage = 0;
    private int maxPage;

    public event Action OnFirstPage;
    public event Action OnLastPage;

    // Start is called before the first frame update
    void Awake()
    {
        if(pages.Length > 0)
        {
            nowPage = 0;
            maxPage = pages.Length - 1;
        }
    }

    /// <summary>
    /// ページUIを開く際の初期セットアップ
    /// </summary>
    public void SetupPage()
    {
        if (pages == null || pages.Length == 0) return;

        // 念のため全てのページを非表示にしてから、目的のページだけを開く（安全対策）
        foreach (var _page in pages)
        {
            if (_page != null) _page.SetActive(false);
        }

        pages[nowPage].SetActive(true);
        CheckPageEvents();
    }

    /// <summary>
    /// 次のページへ進む
    /// </summary>
    public void AdvancePage()
    {
        if (IsLastPage()) return;

        pages[nowPage].SetActive(false);
        nowPage++;
        pages[nowPage].SetActive(true);

        CheckPageEvents();
    }

    /// <summary>
    /// 前のページに戻る
    /// </summary>
    public void ReturnPage()
    {
        if (IsFirstPage()) return;

        pages[nowPage].SetActive(false);
        nowPage--;
        pages[nowPage].SetActive(true);

        CheckPageEvents();
    }
    /// <summary>
    /// ページを閉じる
    /// </summary>
    public void ClosePage()
    {
        pages[nowPage].SetActive(false);
    }

    /// <summary>
    /// 最初のページ
    /// </summary>
    /// <returns></returns>
    public bool IsFirstPage() => nowPage == 0;

    /// <summary>
    /// 最後のページ
    /// </summary>
    /// <returns></returns>
    public bool IsLastPage() => nowPage == maxPage;

    /// <summary>
    /// 現在のページ状態をチェックし、必要ないイベントを発行
    /// </summary>
    private void CheckPageEvents()
    {
        if(IsFirstPage()) OnFirstPage?.Invoke();
        if(IsLastPage()) OnLastPage?.Invoke();
    }
}
