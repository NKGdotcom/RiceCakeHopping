using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NextStageText : OnTextSceneMoveMouse
{
    [SerializeField] private string nextStageName = "Stage";

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        SetFadeAnimation();
        StartCoroutine(WaitStart(nextStageName));
    }
}