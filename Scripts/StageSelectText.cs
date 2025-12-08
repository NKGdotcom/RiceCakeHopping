using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageSelectText : OnTextMouse
{
    [SerializeField] private Animator stageSelectOpen;
    private string gotostageSelectString = "GoToStageSelect";
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        stageSelectOpen.SetBool(gotostageSelectString, true);
    }
}
