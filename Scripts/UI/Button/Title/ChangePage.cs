using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePage : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    private int nowPage = 0;
    private int maxPage;

    public event Action ReachFirstPage;
    public event Action ReachLastPage;

    // Start is called before the first frame update
    void Awake()
    {
        if(pages.Length > 0)
        {
            nowPage = 0;
            maxPage = pages.Length - 1;
        }
    }

    public void SetupPage()
    {
        if (IsLastPage()) ReachLastPage?.Invoke();
        if (IsFirstPage()) ReachFirstPage?.Invoke();
        pages[nowPage].SetActive(true);
    }

    public void AdvancePage()
    {
        pages[nowPage].SetActive(false);
        nowPage++;
        if (IsLastPage()) ReachLastPage?.Invoke();
        pages[nowPage].SetActive(true);
    }

    public void ReturnPage()
    {
        pages[nowPage].SetActive(false);
        nowPage--;
        if (IsFirstPage()) ReachFirstPage?.Invoke();
        pages[nowPage].SetActive(true);
    }

    private bool IsLastPage()
    {
        return nowPage == maxPage;
    }

    public bool IsFirstPage()
    {
        return nowPage == 0;
    }
    public void ClosePage()
    {
        pages[nowPage].SetActive(false);
    }
}
