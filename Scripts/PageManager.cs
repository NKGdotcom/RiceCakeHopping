using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PageType
{
    StageSelectPage, 
    HowToPlayPage
}
public class PageManger : MonoBehaviour
{
    [SerializeField] private PageType pageType; //ページタイプ
    [SerializeField] private GameObject[] pages; //ページの数
    [SerializeField] private GameObject nextButton; //次のページに切り替えるボタン
    [SerializeField] private GameObject prevButton; //前のページに切り替えるボタン

    private static Dictionary<PageType, int> savedPageIndexes = new Dictionary<PageType, int>();
    private int currentIndex = 0;

    private void OnEnable()
    {
        if(savedPageIndexes.ContainsKey(pageType))
        {
            currentIndex = savedPageIndexes[pageType];
        }

        else
        {
            currentIndex = 0;
        }

        UpdatePageDisplay();
    }

    public void ChangePage(int _direction)
    {
        currentIndex += _direction;
        currentIndex = Mathf.Clamp(currentIndex, 0, pages.Length - 1);

        if (savedPageIndexes.ContainsKey(pageType))
        {
            savedPageIndexes[pageType] = currentIndex;
        }
        else
        {
            savedPageIndexes.Add(pageType, currentIndex);
        }
        UpdatePageDisplay();
    }

    private void UpdatePageDisplay()
    {
        for(int i =0; i< pages.Length; i++)
        {
            pages[i].SetActive(i == currentIndex);
        }

        if(prevButton != null) prevButton.SetActive(currentIndex > 0);
        if(nextButton != null) nextButton.SetActive(currentIndex < pages.Length - 1);
    }
}
