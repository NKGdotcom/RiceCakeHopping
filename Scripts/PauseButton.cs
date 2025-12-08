using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : OnMouse
{
    [SerializeField] private Animator pauseButtonAnimator; //ポーズボタンのアニメーション
    [SerializeField] private Animator pauseUIAnimator; //ポーズUIのアニメーション
    private string pauseButtonAnimeStr = "ButtonEnter";
    private string pauseButtonDisplayStr = "DisplayUI";

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        pauseButtonAnimator.SetBool(pauseButtonAnimeStr, true);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        pauseButtonAnimator.SetBool(pauseButtonAnimeStr, false);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        pauseUIAnimator.SetBool(pauseButtonDisplayStr, true);
        Time.timeScale = 0.0f;
    }
}
