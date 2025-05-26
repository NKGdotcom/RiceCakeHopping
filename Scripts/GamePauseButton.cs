using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GamePauseButton : MonoBehaviour
{
    [Header("���̃N���X�Q��")]
    [SerializeField] private StageManager stageManager;
    [SerializeField] private SoundVolume soundVolume;

    [Header("�|�[�Y���")]
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject backToGameTextObj;
    [SerializeField] private TextMeshProUGUI backToGameTMP;
    [SerializeField] private GameObject oneMoreTimeTextObj;
    [SerializeField] private TextMeshProUGUI oneMoreTMP;
    [SerializeField] private GameObject titleTextObj;
    [SerializeField] private TextMeshProUGUI titleTMP;
    private string pauseButtonMouseEnterBool = "PauseButtonMouseEnter";
    private string displayUIBool = "DisplayUI";
    private string fadeOutBool = "FadeOut";
    private float changeColorDuration = 0.1f;
    private float fadeOutWaitTime = 0.5f;

    [Header("���U���g���")]
    [SerializeField] private GameObject nextStageTextObj;
    [SerializeField] private TextMeshProUGUI nextStageTMP;
    [SerializeField] private GameObject resultOneMoreTimeTextObj;
    [SerializeField] private TextMeshProUGUI resultOneMoreTimeTMP;
    [SerializeField] private GameObject resultTitleTextObj;
    [SerializeField] private TextMeshProUGUI resultTitleTMP;

    [Header("�X���C�_�[")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;

    [SerializeField] private Animator pauseUIAnimator;

    private Dictionary<TextMeshProUGUI, Coroutine> colorCoroutines = new Dictionary<TextMeshProUGUI, Coroutine>();

    private string faceColor = "FaceColor";
    private Color32 mouseColorEnter = new Color32(255, 47, 47, 255);
    private Color32 mouseColorExit = new Color32(0, 0, 0, 255);

    private string goToTitleString = "Title";

    // Start is called before the first frame update
    void Start()
    {
        // �|�[�Y�{�^���̐ݒ�
        SetupPauseButton();
        //�|�[�Y��ʂ̃e�L�X�g
        SetupTMPEvents(backToGameTextObj, (x) => BackTMPMouseEnter(), (x) => BackTMPMouseExit(), (x) => BackTMPMouseClick());
        SetupTMPEvents(oneMoreTimeTextObj, (x) => OneMoreTMPMouseEnter(), (x) => OneMoreTMPMouseExit(), (x) => StartCoroutine(OneMoreTMPMouseClick()));
        SetupTMPEvents(titleTextObj, (x) => TitleTMPMouseEnter(), (x) => TitleTMPMouseExit(), (x) => StartCoroutine(TitleTMPMouseClick()));
        //���U���g��ʂ̃e�L�X�g
        SetupTMPEvents(nextStageTextObj, (x) => ResultNextStageTMPMouseEnter(), (x) => ResultNextStageTMPMouseExit(), (x) => StartCoroutine(ResultNextStageTMPMouseClick()));
        SetupTMPEvents(resultOneMoreTimeTextObj, (x) => ResultOneMoreTimeTMPMouseEnter(), (x) => ResultOneMoreTimeTMPMouseExit(), (x) => StartCoroutine(ResultOneMoreTimeTMPMouseClick()));
        SetupTMPEvents(resultTitleTextObj, (x) => ResultTitleTMPMouseEnter(), (x) => ResultTitleTMPMouseExit(), (x) => StartCoroutine(ResultTitleTMPMouseClick()));
    }

    // Update is called once per frame
    void Update()
    {

    }

    // �C�x���g�g���K�[��ǉ�����ėp�֐�
    private void AddEventTrigger(GameObject _target, EventTriggerType _eventType, UnityAction<BaseEventData> _action)
    {
        EventTrigger trigger = _target.GetComponent<EventTrigger>() ?? _target.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = _eventType };
        entry.callback.AddListener(_action);
        trigger.triggers.Add(entry);
    }

    // �|�[�Y�{�^���̐ݒ�
    private void SetupPauseButton()
    {
        AddEventTrigger(gameObject, EventTriggerType.PointerEnter, (x) => PauseMouseEnter());
        AddEventTrigger(gameObject, EventTriggerType.PointerExit, (x) => PauseMouseExit());
        AddEventTrigger(gameObject, EventTriggerType.PointerClick, (x) => PauseMouseClick());
    }

    // �e�L�X�g�{�^���̃C�x���g�ݒ�
    private void SetupTMPEvents(GameObject _target, UnityAction<BaseEventData> _enterAction, UnityAction<BaseEventData> _exitAction, UnityAction<BaseEventData> _clickAction)
    {
        AddEventTrigger(_target, EventTriggerType.PointerEnter, _enterAction);
        AddEventTrigger(_target, EventTriggerType.PointerExit, _exitAction);
        AddEventTrigger(_target, EventTriggerType.PointerClick, _clickAction);
    }
    private IEnumerator ChangeTextColorOverTime(TextMeshProUGUI _text, Color32 _targetColor, float _duration)
    {
        Color32 startColor = _text.color;
        float _elapsed = 0f;

        while (_elapsed < _duration)
        {
            _elapsed += Time.deltaTime;
            float _t = _elapsed / _duration;
            _text.color = Color.Lerp(startColor, _targetColor, _t);
            _text.fontMaterial.SetColor(faceColor, _text.color); // �}�e���A���̐F�����
            yield return null;
        }

        _text.color = _targetColor; // �ŏI�F�ɐݒ�
        _text.fontMaterial.SetColor(faceColor, _targetColor);
    }
    private void StartColorChange(TextMeshProUGUI _text, Color32 _targetColor)
    {
        if (colorCoroutines.TryGetValue(_text, out Coroutine coroutine))
        {
            if (coroutine != null) StopCoroutine(coroutine);
        }
        Coroutine newCoroutine = StartCoroutine(ChangeTextColorOverTime(_text, _targetColor, changeColorDuration));
        colorCoroutines[_text] = newCoroutine;
    }
    //�|�[�Y�{�^��
    private void PauseMouseEnter()
    {
        pauseUIAnimator.SetBool(pauseButtonMouseEnterBool, true);
    }
    private void PauseMouseExit()
    {
        pauseUIAnimator.SetBool(pauseButtonMouseEnterBool,false);
    }
    private void PauseMouseClick()
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        stageManager.IsPause = true;
        pauseUI.SetActive(true);
        pauseUIAnimator.SetBool(displayUIBool, true);
        pauseUIAnimator.SetBool(pauseButtonMouseEnterBool, false);
    }
    //�|�[�Y���̃Q�[���ɖ߂�{�^��
    private void BackTMPMouseEnter()
    {
        StartColorChange(backToGameTMP, mouseColorEnter);
    }
    private void BackTMPMouseExit()
    {
        StartColorChange(backToGameTMP,mouseColorExit);
    }
    private void BackTMPMouseClick()
    {
        stageManager.IsPause = false;
        SoundManager.Instance.PlaySE(SESource.backButton);
        pauseUIAnimator.SetBool(displayUIBool, false);
    }
    private void OneMoreTMPMouseEnter()
    {
        StartColorChange(oneMoreTMP, mouseColorEnter);
    }
    //�|�[�Y���̂�����x�{�^��
    private void OneMoreTMPMouseExit()
    {
        StartColorChange(oneMoreTMP, mouseColorExit);
    }
    private IEnumerator OneMoreTMPMouseClick()
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        pauseUIAnimator.SetBool(fadeOutBool, true);
        // �����҂��Ă���V�[���J�ځi��: �t�F�[�h�A�E�g�҂��Ȃǁj
        yield return new WaitForSeconds(fadeOutWaitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //�|�[�Y���̃^�C�g���֖߂�{�^��
    private void TitleTMPMouseEnter()
    {
        StartColorChange(titleTMP, mouseColorEnter);
    }
    private void TitleTMPMouseExit()
    {
        StartColorChange(titleTMP, mouseColorExit);
    }
    private IEnumerator TitleTMPMouseClick()
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        pauseUIAnimator.SetBool(fadeOutBool, true);
        // �����҂��Ă���V�[���J�ځi��: �t�F�[�h�A�E�g�҂��Ȃǁj
        yield return new WaitForSeconds(fadeOutWaitTime);
        SceneManager.LoadScene(goToTitleString);
    }
    //���U���g���̃X�e�[�W�֍s���{�^��
    private void ResultNextStageTMPMouseEnter()
    {
        StartColorChange(nextStageTMP, mouseColorEnter);
    }
    private void ResultNextStageTMPMouseExit()
    {
        StartColorChange(nextStageTMP, mouseColorExit);
    }
    private IEnumerator ResultNextStageTMPMouseClick()
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        pauseUIAnimator.SetBool(fadeOutBool, true);
        // �����҂��Ă���V�[���J�ځi��: �t�F�[�h�A�E�g�҂��Ȃǁj
        yield return new WaitForSeconds(fadeOutWaitTime);
        SceneManager.LoadScene($"Stage{stageManager.StageNum + 1}");
    }
    //���U���g������x�{�^��
    private void ResultOneMoreTimeTMPMouseEnter()
    {
        StartColorChange(resultOneMoreTimeTMP, mouseColorEnter);
    }
    private void ResultOneMoreTimeTMPMouseExit()
    {
        StartColorChange(resultOneMoreTimeTMP, mouseColorExit);
    }
    private IEnumerator ResultOneMoreTimeTMPMouseClick()
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        pauseUIAnimator.SetBool(fadeOutBool, true);
        // �����҂��Ă���V�[���J�ځi��: �t�F�[�h�A�E�g�҂��Ȃǁj
        yield return new WaitForSeconds(fadeOutWaitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void ResultTitleTMPMouseEnter()
    {
        StartColorChange(resultTitleTMP, mouseColorEnter);
    }
    private void ResultTitleTMPMouseExit()
    {
        StartColorChange(resultTitleTMP, mouseColorExit);
    }
    private IEnumerator ResultTitleTMPMouseClick()
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        pauseUIAnimator.SetBool(fadeOutBool, true);
        // �����҂��Ă���V�[���J�ځi��: �t�F�[�h�A�E�g�҂��Ȃǁj
        yield return new WaitForSeconds(fadeOutWaitTime);
        SceneManager.LoadScene(goToTitleString);
    }
}
