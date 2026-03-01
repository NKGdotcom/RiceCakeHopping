using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    [SerializeField] private RightButtonArrowContoroller rightButtonArrowController;
    [SerializeField] private LeftButtonArrowController leftButtonArrowController;
    [SerializeField] private ChangePage changePage;
    [SerializeField] private BackTitleTextController backTitleTextController;

    void Start()
    {
        if (rightButtonArrowController == null) { Debug.LogError("rightButtonArrowControllerが参照されていません。"); }
        if (leftButtonArrowController == null) { Debug.LogError("leftButtonArrowControllerが参照されていません。"); return;}
        if (changePage == null) { Debug.LogError("changePageが参照されていません。"); return; }
        if(backTitleTextController == null) { Debug.LogError("backTitleTextController"); return; }

        rightButtonArrowController.ProceedPage += ProceedPage;
        leftButtonArrowController.BackPage += BackPage;
        changePage.ReachLastPage += ReachLastPage;
        changePage.ReachFirstPage += ReachFirstPage;
        backTitleTextController.IsClicked += ClosePage;
    }

    public void SetPage()
    {
        rightButtonArrowController.gameObject.SetActive(true);
        leftButtonArrowController.gameObject.SetActive(true);
        changePage.SetupPage();
    }

    public void ProceedPage()
    {
        leftButtonArrowController.gameObject.SetActive(true);
        rightButtonArrowController.gameObject.SetActive(true);
        changePage.AdvancePage();
    }

    public void BackPage()
    {
        leftButtonArrowController.gameObject.SetActive(true);
        rightButtonArrowController.gameObject.SetActive(true);
        changePage.ReturnPage();
    }

    public void ReachLastPage()
    {
        rightButtonArrowController.gameObject.SetActive(false);
    }

    public void ReachFirstPage()
    {
        leftButtonArrowController.gameObject.SetActive(false);
    }

    public void ClosePage()
    {
        changePage.ClosePage();
    }
}
