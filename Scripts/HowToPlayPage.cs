using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayPage : BaseUIPage
{
    public static HowToPlayPage Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    protected override void Start()
    {
        base.Start();
    }
    public new void OpenPage()
    {
        TitleAnimationState.Instance.MoveToHowToPlayPage();
        base.OpenPage();
    }
    public new void NextPage()
    {
        base.NextPage();
    }
    public new void BackPage()
    {
        base.BackPage();
    }
    public new void ClosePage()
    {
        TitleAnimationState.Instance.MoveToTitleNext();
        base.ClosePage();
    }
}
