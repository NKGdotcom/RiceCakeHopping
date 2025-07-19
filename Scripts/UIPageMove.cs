using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// �^�C�g��UI�Ńy�[�W��ς��鎞�̏���
/// </summary>
public class UIPageMove : TextColorChange
{
    [Header("�y�[�W�Ɉڂ邽�߂̃e�L�X�g")]
    [SerializeField] protected TextMeshProUGUI movePageText;

    //�e�L�X�g���N���b�N�����ۂɎ��s����֐�
    protected UnityAction<BaseEventData> movePage;

    [Header("�y�[�W���X�g")]
    [SerializeField] protected List<GameObject> uiPageList; //UI�̃y�[�W���X�g
    [Header("�y�[�W��i�߂���߂����")]
    [SerializeField] protected GameObject rightArrow; //�E���
    [SerializeField] protected GameObject leftArrow;  //�����
    [Header("�ЂƂO�ɖ߂�e�L�X�g")]
    [SerializeField] protected TextMeshProUGUI backPageText;

    private float arrowMoveSpeed = 25f; //��󂪏㉺�ɓ�������

    protected int maxPageNum;  //�y�[�W��
    protected int currentPage; //���݂̃y�[�W

    private Vector3 initialArrowPos; //�}�E�X���甲�����Ƃ��Ɍ��̈ʒu�ɖ߂�

    private Coroutine currentArrowAnimation; // ���ݎ��s���̖��A�j���[�V�����R���[�`��

    // Start is called before the first frame update
    protected override void Start()
    {

        rightArrow.SetActive(false);
        leftArrow.SetActive(false);

        maxPageNum = uiPageList.Count - 1; //���X�g��0����n�܂邽��
        // �y�[�W���߂�����
        SetUpArrowEvent(rightArrow,
                        (eventData) => { currentArrowAnimation = StartCoroutine(ArrowEnter(rightArrow)); },
                        (eventData) => { ArrowExit(rightArrow); },
                        (eventData) => { TurnThePage(); ArrowExit(rightArrow); });
        //�y�[�W��߂����
        SetUpArrowEvent(leftArrow,
                        (eventData) => { currentArrowAnimation = StartCoroutine(ArrowEnter(leftArrow)); },
                        (eventData) => { ArrowExit(leftArrow); },
                        (eventData) => { GoBackPage(); ArrowExit(leftArrow); });
        //�y�[�W���J���e�L�X�g
        SetUpTextEvent(movePageText.gameObject,
                        (eventData) => { ChangeWhiteText(movePageText); },
                        (eventData) => { ResetWhiteText(movePageText); },
                        (eventData) => {
                            movePage?.Invoke(eventData);
                            OpenPage();
                        });
        //�y�[�W�����e�L�X�g
        SetUpTextEvent(backPageText.gameObject,
                        (eventData) => { ChangeBlackText(backPageText); },
                        (eventData) => { ResetBlackText(backPageText); },
                        (eventData) => { ClosePage(); });
    }

    // Update is called once for frame
    void Update()
    {

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

        if (currentPage == maxPageNum) rightArrow.SetActive(false); //�Ō�̃y�[�W�̎�
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