using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HowToPlayText : OnTextMouse
{
    [SerializeField] private Animator howtoplayOpen;
    private string gotoHowToPlayString = "GoToHowToPlay";
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        howtoplayOpen.SetBool(gotoHowToPlayString, true);
    }
}
