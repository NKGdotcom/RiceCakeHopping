using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackGameText : OnTextMouse
{
    [SerializeField] private Animator pauseAnimator;
    private string pauseButtonDisplayStr = "DisplayUI";

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        pauseAnimator.SetBool(pauseButtonDisplayStr, false);
        Time.timeScale = 1.0f;
    }
}
