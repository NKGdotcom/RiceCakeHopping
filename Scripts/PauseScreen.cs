using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ポーズ画面(リザルト画面継承)
/// </summary>
public class PauseScreen : ResultScreen
{
    [Header("ポーズボタン")]
    [SerializeField] private Image pauseImage;
    [Header("ポーズ画面")]
    [SerializeField] private TextMeshProUGUI backGameTMP;
    [SerializeField] private TextMeshProUGUI retryGameTMP;
    [SerializeField] private TextMeshProUGUI backTitleTMP;

    // Start is called before the first frame update
    new void Start()
    {
        SetUpTextEvent(backGameTMP.gameObject, //ゲーム画面に戻る
                        (eventData) => { ChangeBlackText(backGameTMP); },
                        (eventData) => { ResetBlackText(backGameTMP); },
                        (eventData) => {
                            ClosePausePage();
                        });
        SetUpTextEvent(retryGameTMP.gameObject, //もう一度同じステージ
                        (eventData) => { ChangeBlackText(retryGameTMP); },
                        (eventData) => { ResetBlackText(retryGameTMP); },
                        (eventData) => {
                            StartCoroutine(ChangeScene(SceneManager.GetActiveScene().name));
                        });
        SetUpTextEvent(backTitleTMP.gameObject, //タイトルへ戻る
                        (eventData) => { ChangeBlackText(backTitleTMP); },
                        (eventData) => { ResetBlackText(backTitleTMP); },
                        (eventData) => {
                            StartCoroutine(ChangeScene("Title"));
                        });
        SetUpPauseImageEvent(pauseImage.gameObject,
                        (eventData) => { fadeOutAnimator.SetBool("PauseButtonMouseEnter", true); },
                        (eventData) => { fadeOutAnimator.SetBool("PauseButtonMouseEnter", false); },
                        (eventData) => {
                            OpenPausePage();
                        });
    }
    /// <summary>
    /// ポーズ画面を開く
    /// </summary>
    private void OpenPausePage()
    {
        fadeOutAnimator.SetBool("DisplayUI", true);
        GameStateMachine.Instance.SetState(GameStateMachine.GameState.Pause);

        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
    }
    /// <summary>
    /// ポーズ画面を閉じる
    /// </summary>
    private void ClosePausePage()
    {
        fadeOutAnimator.SetBool("DisplayUI", false);
        GameStateMachine.Instance.SetState(GameStateMachine.GameState.Playing);

        SoundManager.Instance.PlaySE(SESource.backButton);
    }
    private void SetUpPauseImageEvent(GameObject _image, UnityAction<BaseEventData> _enterAction, UnityAction<BaseEventData> _exitAction, UnityAction<BaseEventData> _clickAction)
    {
        AddEventTrigger(_image, EventTriggerType.PointerEnter, _enterAction);
        AddEventTrigger(_image, EventTriggerType.PointerExit, _exitAction);
        AddEventTrigger(_image, EventTriggerType.PointerClick, _clickAction);
    }
}

