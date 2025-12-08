using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RetryText : OnTextSceneMoveMouse
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        SetFadeAnimation();
        StartCoroutine(WaitStart(SceneManager.GetActiveScene().name));
    }
}
