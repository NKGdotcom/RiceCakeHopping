using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseUIPage : MonoBehaviour
{
    [SerializeField] protected List<GameObject> uiPageList;
    [SerializeField] protected GameObject rightArrow;
    [SerializeField] protected GameObject leftArrow;
    protected int currentPageNum;
    protected int maxPageNum;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);

        maxPageNum = uiPageList.Count - 1;
    }
    protected void OpenPage()
    {
        if (currentPageNum != maxPageNum) rightArrow.SetActive(true);
        if(currentPageNum != 0) leftArrow.SetActive(true);

        SoundManager.Instance.PlaySE(SESource.riceCakeCollision);
    }

    protected void NextPage()
    {
        uiPageList[currentPageNum].SetActive(false);
        currentPageNum++;
        uiPageList[currentPageNum].SetActive(true);
        leftArrow.SetActive(true);

        if (currentPageNum >= maxPageNum) rightArrow.SetActive(false);
        else rightArrow.SetActive(true);

        SoundManager.Instance.PlaySE(SESource.riceCakeCollision);
    }
    protected void BackPage()
    {
        uiPageList[currentPageNum].SetActive(false);
        currentPageNum--;
        uiPageList[currentPageNum].SetActive(true);
        rightArrow.SetActive(true);

        if(currentPageNum == 0) leftArrow.SetActive(false);
        else leftArrow.SetActive(true);

        SoundManager.Instance.PlaySE(SESource.backButton);
    }

    protected void ClosePage()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);

        TitleAnimationState.Instance.MoveToTitleNext();
        SoundManager.Instance.PlaySE(SESource.backButton);
    }
}
