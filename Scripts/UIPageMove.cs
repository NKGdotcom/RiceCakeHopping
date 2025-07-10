using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// �^�C�g��UI�Ńy�[�W��ς��鎞�̏���
/// </summary>
public class UIPageMove : MonoBehaviour
{
    [Header("�y�[�W�Ɉڂ邽�߂̃e�L�X�g")]
    [SerializeField] protected TextMeshProUGUI movePageText;
    protected float colorChangeDuration = 0.1f;
    private string textColor = "FaceColor";
    private static readonly Color32 highlightColor = new Color32(255, 130, 130, 255);//�e�L�X�g�̏�ɒu�����班���Ԃ��ۂ��F��
    private static readonly Color32 defaultColor = new Color32(255, 255, 255, 255);//�e�L�X�g����O�ꂽ��
    //�e�L�X�g���N���b�N�����ۂɎ��s����֐�
    protected UnityAction<BaseEventData> movePage;

    [Header("�y�[�W���X�g")]
    [SerializeField] protected List<GameObject> uiPageList; //UI�̃y�[�W���X�g
    [Header("�y�[�W��i�߂���߂����")]
    [SerializeField] protected GameObject rightArrow; //�E���
    [SerializeField] protected GameObject leftArrow;  //�����
    [Header("�ЂƂO�ɖ߂�e�L�X�g")]
    [SerializeField] protected TextMeshProUGUI backPageText;
    private static readonly Color32 backTextDefaultColor = new Color32(0,0,0,255);

    private float arrowMoveSpeed = 25f; //��󂪏㉺�ɓ�������

    protected int maxPageNum;  //�y�[�W��
    protected int currentPage; //���݂̃y�[�W

    private Vector3 initialArrowPos; //�}�E�X���甲�����Ƃ��Ɍ��̈ʒu�ɖ߂�

    private Coroutine currentColorChangeCoroutine; //���ݎ��s���̐F���ς��A�j���[�V�����R���[�`��
    private Coroutine currentArrowAnimation; // ���ݎ��s���̖��A�j���[�V�����R���[�`��

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);

        maxPageNum = uiPageList.Count - 1; //���X�g��0����n�܂邽��
        // �y�[�W���߂�����
        SetUpArrowEvent(rightArrow,
                        (eventData) => { currentArrowAnimation = StartCoroutine(ArrowEnter(rightArrow)); },
                        (eventData) => { ArrowExit(rightArrow); },
                        (eventData) => { TurnThePage(); ArrowExit(rightArrow);});
        //�y�[�W��߂����
        SetUpArrowEvent(leftArrow,
                        (eventData) => { currentArrowAnimation = StartCoroutine(ArrowEnter(leftArrow)); },
                        (eventData) => { ArrowExit(leftArrow); },
                        (eventData) => { GoBackPage(); ArrowExit(leftArrow); });
        //�y�[�W���J���e�L�X�g
        SetUpTextEvent(movePageText.gameObject,
                        (eventData) => { StartColorChange(movePageText, highlightColor); },
                        (eventData) => { StartColorChange(movePageText, defaultColor); },
                        (eventData) => {
                            movePage?.Invoke(eventData);
                            OpenPage();
                        });
        //�y�[�W�����e�L�X�g
        SetUpTextEvent(backPageText.gameObject,
                        (eventData) => { StartColorChange(backPageText, highlightColor); },
                        (eventData) => { StartColorChange(backPageText, backTextDefaultColor); },
                        (eventData) => { ClosePage(); });
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// �y�[�W�Ɉڂ�e�L�X�g�̏�����ݒ�
    /// </summary>
    /// <param name="_text">�e�L�X�g</param>
    /// <param name="_enterAction">�e�L�X�g�̏�Ƀ}�E�X��u�����Ƃ�</param>
    /// <param name="_exitAction">�e�L�X�g����}�E�X�𗣂����Ƃ�</param>
    /// <param name="_clickAction">�e�L�X�g���N���b�N������</param>
    protected void SetUpTextEvent(GameObject _text, UnityAction<BaseEventData> _enterAction, UnityAction<BaseEventData> _exitAction, UnityAction<BaseEventData> _clickAction)
    {
        AddEventTrigger(_text, EventTriggerType.PointerEnter, _enterAction);
        AddEventTrigger(_text, EventTriggerType.PointerExit, _exitAction);
        AddEventTrigger(_text, EventTriggerType.PointerClick, _clickAction);
    }
    /// <summary>
    /// �e�L�X�g�̐F�����X�ɕς��鏈��
    /// </summary>
    /// <param name="_text">�e�L�X�g</param>
    /// <param name="_targetColor">�ǂ̐F�ɕς��邩</param>
    /// <param name="_duration">�F���ς�鎞��</param>
    /// <returns></returns>
    private IEnumerator ChangeTextColorOverTime(TextMeshProUGUI _text, Color32 _targetColor, float _duration)
    {
        Color32 startColor = _text.color;
        float _elapsed = 0f;

        while (_elapsed < _duration)
        {
            _elapsed += Time.deltaTime;
            float _t = _elapsed / _duration;
            _text.color = Color.Lerp(startColor, _targetColor, _t);
            _text.fontMaterial.SetColor(textColor, _text.color); // �}�e���A���̐F�����
            yield return null;
        }

        _text.color = _targetColor; // �ŏI�F�ɐݒ�
        _text.fontMaterial.SetColor(textColor, _targetColor);
    }
    /// <summary>
    /// �e�L�X�g�̐F��ς���
    /// </summary>
    /// <param name="_text">�e�L�X�g</param>
    /// <param name="_changeColor">�ύX����F</param>
    protected void StartColorChange(TextMeshProUGUI _text, Color32 _changeColor)
    {
        currentColorChangeCoroutine = StartCoroutine(ChangeTextColorOverTime(_text, _changeColor, colorChangeDuration));
    }

    /// <summary>
    /// ���L�[�̃C�x���g�g���K�[�̃Z�b�g�A�b�v
    /// </summary>
    /// <param name="_arrow">���</param>
    /// <param name="_enterAction">�}�E�X�̏�ɒu������N���鏈��</param>
    /// <param name="_exitAction">�}�E�X���痣������N���鏈��</param>
    /// <param name="_clickAction">�N���b�N������N���鏈��</param>
    private void SetUpArrowEvent(GameObject _arrow, UnityAction<BaseEventData> _enterAction, UnityAction<BaseEventData> _exitAction, UnityAction<BaseEventData> _clickAction)
    {
        AddEventTrigger(_arrow, EventTriggerType.PointerEnter, _enterAction);
        AddEventTrigger(_arrow, EventTriggerType.PointerExit, _exitAction);
        AddEventTrigger(_arrow, EventTriggerType.PointerClick, _clickAction);
    }

    /// <summary>
    /// �C�x���g�g���K�[�̏���������
    /// </summary>
    /// <param name="_target">UI</param>
    /// <param name="_eventType">�C�x���g�g���K�[�̉����g����</param>
    /// <param name="_action">���s�������֐�</param>
    protected void AddEventTrigger(GameObject _target, EventTriggerType _eventType, UnityAction<BaseEventData> _action)
    {
        EventTrigger _trigger = _target.GetComponent<EventTrigger>() ?? _target.AddComponent<EventTrigger>();
        EventTrigger.Entry _entry = new EventTrigger.Entry { eventID = _eventType };
        _entry.callback.AddListener(_action);
        _trigger.triggers.Add(_entry);
    }

    /// <summary>
    /// �}�E�X�̏�ɓ�������N���鏈�� (���̏㉺�A�j���[�V�������J�n)
    /// </summary>
    /// <returns></returns>
    private IEnumerator ArrowEnter(GameObject _arrow)
    {
        initialArrowPos = _arrow.transform.localPosition;
        float _time = 0f;
        while (true)
        {
            float _offset = Mathf.Sin(_time * arrowMoveSpeed);
            _arrow.transform.localPosition = initialArrowPos + new Vector3(0, _offset, 0);
            _time += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// �}�E�X���痣�ꂽ��N���鏈�� (���̏㉺�A�j���[�V�������~���A���̈ʒu�ɖ߂�)
    /// </summary>
    private void ArrowExit(GameObject _arrow)
    {
        if (currentArrowAnimation != null)
        {
            StopCoroutine(currentArrowAnimation);
            _arrow.transform.localPosition = initialArrowPos;
        }
    }

    /// <summary>
    /// �y�[�W���߂���
    /// </summary>
    protected void TurnThePage()
    {
        uiPageList[currentPage].SetActive(false);
        currentPage++;
        uiPageList[currentPage].SetActive(true);
        leftArrow.SetActive(true);

        if(currentPage == maxPageNum) rightArrow.SetActive(false); //�Ō�̃y�[�W�̎�
        else rightArrow.SetActive(true);

        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
    }

    /// <summary>
    /// �y�[�W��߂�
    /// </summary>
    protected void GoBackPage()
    {
        uiPageList[currentPage].SetActive(false);
        currentPage--;
        uiPageList[currentPage].SetActive(true);
        rightArrow.SetActive(true);

        if (currentPage == 0) leftArrow.SetActive(false); //�ŏ��̃y�[�W�̎�
        else rightArrow.SetActive(true);

        SoundManager.Instance.PlaySE(SESource.backButton);
    }
    /// <summary>
    /// �y�[�W���J��
    /// </summary>
    protected void OpenPage()
    {
        if (currentPage != maxPageNum) rightArrow.SetActive(true);
        if (currentPage != 0) leftArrow.SetActive(true);

        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
    }
    /// <summary>
    /// �y�[�W�����
    /// </summary>
    protected void ClosePage()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);

        TitleAnimationState.Instance.MoveToTitleNext();
        SoundManager.Instance.PlaySE(SESource.backButton);
    }
}