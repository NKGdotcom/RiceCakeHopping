using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// タイトルの遊び方ページをUIPageMoveを用いて継承する
/// </summary>
public class PageHowToPlay : UIPageMove
{
    private void Awake()
    {
        movePage = (eventData) => { TitleAnimationState.Instance.MoveToHowToPlayPage(); };
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
}
