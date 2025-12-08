using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//テキストのベーススクリプト
public class OnTextMouse : OnMouse
{
    [SerializeField] private Animator changeTextAnimator;
    private string changeColor = "ChangeRed";

    /// <summary>
    /// テキストの上にマウスを置いたら色を変える
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        changeTextAnimator.SetBool(changeColor, true);
    }

    /// <summary>
    /// テキストの上にマウスを離したら色をもとに戻す
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        changeTextAnimator.SetBool(changeColor, false);
    }

    /// <summary>
    /// テキストをクリックしたら
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }
}
