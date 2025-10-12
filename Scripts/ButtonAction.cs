using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAction : MonoBehaviour, IPointerClickHandler
{
    public enum ButtonVaraiety {TitleStageSelect, TitleHowToPlay,StageSelectText,StageSelectBackTitle, HowToPlayBackTitle,
                                PauseBackGame,PauseRetry,PauseBackTitle,PauseOpen,
                                ResultNextStage,ResultRetry,ResultTitle}

    [Header("ƒ{ƒ^ƒ“‚ÌŽí—Þ")]
    [SerializeField] private ButtonVaraiety buttonVaraiety;
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (buttonVaraiety)
        {
            case ButtonVaraiety.TitleStageSelect:
                StageSelectPage.Instance.OpenPage();
                break;
            case ButtonVaraiety.TitleHowToPlay:
                HowToPlayPage.Instance.OpenPage();
                break;
            case ButtonVaraiety.StageSelectText:
                TextMeshProUGUI _text = GetComponent<TextMeshProUGUI>();
                StageSelectPage.Instance.GoToStage(_text);
                break;
            case ButtonVaraiety.StageSelectBackTitle:
                StageSelectPage.Instance.ClosePage();
                break;
            case ButtonVaraiety.HowToPlayBackTitle:
                HowToPlayPage.Instance.ClosePage();
                break;
            case ButtonVaraiety.PauseOpen:
                PauseUIAnimationState.Instance.OpenPause();
                GameStateMachine.Instance.SetState(GameStateMachine.GameState.Pause); ;
                break;
            case ButtonVaraiety.PauseBackGame:
                PauseUIAnimationState.Instance.ClosePause();
                GameStateMachine.Instance.SetState(GameStateMachine.GameState.Playing); ;
                break;
            case ButtonVaraiety.PauseRetry:
                PauseUIAnimationState.Instance.PauseRetry();
                break;
            case ButtonVaraiety.PauseBackTitle:
                PauseUIAnimationState.Instance.PauseBackTitle();
                break;
            case ButtonVaraiety.ResultNextStage:
                ResultUIAnimationState.Instance.ResultNextStage();
                break;
            case ButtonVaraiety.ResultRetry:
                ResultUIAnimationState.Instance.ResultRetry();
                break;
            case ButtonVaraiety.ResultTitle:
                ResultUIAnimationState.Instance.ResultBackTitle();
                break;
        }
    }
}
