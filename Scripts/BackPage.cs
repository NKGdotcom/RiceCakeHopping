using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackPage : OnTextMouse
{
    [SerializeField] private Animator titleAnimator;
    [SerializeField] private string falseStr = "GoTo";
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        titleAnimator.SetBool(falseStr, false);
    }
}
