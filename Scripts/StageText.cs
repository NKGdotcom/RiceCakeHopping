using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageText : OnTextSceneMoveMouse
{
    [SerializeField] private string stageNameString = "Stage";

    private string gameStartTriggerString = "GameStart";
    //private float fadeOutSpeed = 0.5f;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        SetFadeAnimation(gameStartTriggerString);
        StartCoroutine(WaitStart(stageNameString));
    }
}
