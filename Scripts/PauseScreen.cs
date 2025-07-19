using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// �|�[�Y���(���U���g��ʌp��)
/// </summary>
public class PauseScreen : ResultScreen
{
    [Header("�|�[�Y�{�^��")]
    [SerializeField] private Image pauseImage;
    [Header("�|�[�Y���")]
    [SerializeField] private TextMeshProUGUI backGameTMP;
    [SerializeField] private TextMeshProUGUI retryGameTMP;
    [SerializeField] private TextMeshProUGUI backTitleTMP;

    // Start is called before the first frame update
    new void Start()
    {
        SetUpTextEvent(backGameTMP.gameObject, //�Q�[����ʂɖ߂�
                        (eventData) => { ChangeBlackText(backGameTMP); },
                        (eventData) => { ResetBlackText(backGameTMP); },
                        (eventData) => {
                            ClosePausePage();
                        });
        SetUpTextEvent(retryGameTMP.gameObject, //������x�����X�e�[�W
                        (eventData) => { ChangeBlackText(retryGameTMP); },
                        (eventData) => { ResetBlackText(retryGameTMP); },
                        (eventData) => {
                            StartCoroutine(ChangeScene(SceneManager.GetActiveScene().name));
                        });
        SetUpTextEvent(backTitleTMP.gameObject, //�^�C�g���֖߂�
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
    /// �|�[�Y��ʂ��J��
    /// </summary>
    private void OpenPausePage()
    {
        fadeOutAnimator.SetBool("DisplayUI", true);
        GameStateMachine.Instance.SetState(GameStateMachine.GameState.Pause);

        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
    }
    /// <summary>
    /// �|�[�Y��ʂ����
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

