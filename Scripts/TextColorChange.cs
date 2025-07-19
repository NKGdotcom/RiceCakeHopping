using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TextColorChange:MonoBehaviour
{
    private string textColor = "FaceColor";

    protected float colorChangeDuration = 0.1f;
    private Color32 defaultBlackTextColor = new Color32(0, 0, 0, 255);
    private Color32 changeBlackColorText = new Color32(255, 130, 130, 255); // �����e�L�X�g���z�o�[�����Ƃ��ɕς��F
    private Color32 defaultWhiteTextColor = new Color32(255, 255, 255, 255);
    private Color32 changeWhiteColorText = new Color32(255, 130, 130, 255); // �����e�L�X�g���z�o�[�����Ƃ��ɕς��F

    private Coroutine currentColorChangeCoroutine; //���ݎ��s���̐F���ς��A�j���[�V�����R���[�`��

    // Start is called before the first frame update
    protected virtual void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// �C�x���g�g���K�[�̏�����ǉ����܂��B
    /// </summary>
    /// <param name="_target">UI GameObject</param>
    /// <param name="_eventType">�g�p����C�x���g�g���K�[�̃^�C�v</param>
    /// <param name="_action">���s�������֐�</param>
    protected void AddEventTrigger(GameObject _target, EventTriggerType _eventType, UnityAction<BaseEventData> _action)
    {
        EventTrigger _trigger = _target.GetComponent<EventTrigger>() ?? _target.AddComponent<EventTrigger>();
        EventTrigger.Entry _entry = new EventTrigger.Entry { eventID = _eventType };
        _entry.callback.AddListener(_action);
        _trigger.triggers.Add(_entry);
    }
    /// <summary>
    /// �e�L�X�g�̃C�x���g������ݒ肵�܂��B
    /// </summary>
    /// <param name="_text">�Ώۂ̃e�L�X�gGameObject</param>
    /// <param name="_enterAction">�e�L�X�g�̏�Ƀ}�E�X���u���ꂽ�Ƃ��Ɏ��s�����A�N�V����</param>
    /// <param name="_exitAction">�e�L�X�g����}�E�X�����ꂽ�Ƃ��Ɏ��s�����A�N�V����</param>
    /// <param name="_clickAction">�e�L�X�g���N���b�N���ꂽ�Ƃ��Ɏ��s�����A�N�V����</param>
    protected void SetUpTextEvent(GameObject _text, UnityAction<BaseEventData> _enterAction, UnityAction<BaseEventData> _exitAction, UnityAction<BaseEventData> _clickAction)
    {
        AddEventTrigger(_text, EventTriggerType.PointerEnter, _enterAction);
        AddEventTrigger(_text, EventTriggerType.PointerExit, _exitAction);
        AddEventTrigger(_text, EventTriggerType.PointerClick, _clickAction);
    }

    /// <summary>
    /// �e�L�X�g�̐F�����X�ɕύX����R���[�`���ł��B
    /// </summary>
    /// <param name="_text">�Ώۂ�TextMeshProUGUI�R���|�[�l���g</param>
    /// <param name="_targetColor">�ύX��̖ڕW�̐F</param>
    /// <param name="_duration">�F�����S�ɕς��܂ł̎���</param>
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
            // �}�e���A���̐F����Ԃ��邱�ƂŁA�e�L�X�g�S�̂̐F�����炩�ɕς��悤�ɂ��܂�
            _text.fontMaterial.SetColor(textColor, _text.color);
            yield return null;
        }

        // �ŏI�I�ȐF�ɐݒ肵�āA��Ԃ̌덷���Ȃ����܂�
        _text.color = _targetColor;
        _text.fontMaterial.SetColor(textColor, _targetColor);
    }

    /// <summary>
    /// �e�L�X�g�̐F��ύX����R���[�`�����J�n���܂��B
    /// �����̐F�ύX�R���[�`��������ꍇ�́A������~���Ă���V�����R���[�`�����J�n���܂��B
    /// ����ɂ��A�����̐F�ύX�������ɑ���̂�h���A�X���[�Y�ȑJ�ڂ�ۏ؂��܂��B
    /// </summary>
    /// <param name="_text">�Ώۂ�TextMeshProUGUI�R���|�[�l���g</param>
    /// <param name="_targetColor">�ڕW�̐F</param>
    protected void StartColorChange(TextMeshProUGUI _text, Color32 _targetColor)
    {
        currentColorChangeCoroutine = StartCoroutine(ChangeTextColorOverTime(_text, _targetColor, colorChangeDuration));
    }

    /// <summary>
    /// �����e�L�X�g�̐F��ω�������
    /// </summary>
    /// <param name="_text">�Ώۂ�TextMeshProUGUI�R���|�[�l���g</param>
    public void ChangeBlackText(TextMeshProUGUI _text)
    {
        StartColorChange(_text, changeBlackColorText);
    }

    /// <summary>
    /// ���F�ɖ߂��܂��B
    /// </summary>
    /// <param name="_text">�Ώۂ�TextMeshProUGUI�R���|�[�l���g</param>
    public void ResetBlackText(TextMeshProUGUI _text)
    {
        StartColorChange(_text, defaultBlackTextColor);
    }

    /// <summary>
    /// �����e�L�X�g�̐F��ω�������B
    /// </summary>
    /// <param name="_text">�Ώۂ�TextMeshProUGUI�R���|�[�l���g</param>
    public void ChangeWhiteText(TextMeshProUGUI _text)
    {
        StartColorChange(_text, changeWhiteColorText);
    }

    /// <summary>
    /// ���F�ɖ߂��B
    /// </summary>
    /// <param name="_text">�Ώۂ�TextMeshProUGUI�R���|�[�l���g</param>
    public void ResetWhiteText(TextMeshProUGUI _text)
    {
        StartColorChange(_text, defaultWhiteTextColor);
    }
}