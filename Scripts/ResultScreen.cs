using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���U���g���(UI�y�[�W���)
/// </summary>
public class ResultScreen : TextColorChange
{
    protected SetClearConditions setClearConditions;

    [Header("�t�F�[�h�A�E�g�p�̃A�j���[�V�����A��̂��͓̂���Ȃ��đ��v")]
    [SerializeField] protected Animator fadeOutAnimator;

    protected float stageNum;
    protected float fadeOutWaitTime = 0.5f;
    // Start is called before the first frame update
    new protected virtual void Start()
    {
        setClearConditions = GetComponent<SetClearConditions>();
        stageNum = setClearConditions.StageIndexNum;
        SetUpTextEvent(GameResult.Instance.ResultNextSceneTMP.gameObject, //���̃X�e�[�W��
                        (eventData) => { ChangeBlackText(GameResult.Instance.ResultNextSceneTMP); },
                        (eventData) => { ResetBlackText(GameResult.Instance.ResultNextSceneTMP); },
                        (eventData) => {
                            StartCoroutine(ChangeScene($"Stage{stageNum + 1}"));
                        });
        SetUpTextEvent(GameResult.Instance.ResultOneMoreTimeTMP.gameObject, //������x�����X�e�[�W
                        (eventData) => { ChangeBlackText(GameResult.Instance.ResultOneMoreTimeTMP); },
                        (eventData) => { ResetBlackText(GameResult.Instance.ResultOneMoreTimeTMP); },
                        (eventData) => {
                            StartCoroutine(ChangeScene($"Stage{stageNum}"));
                        });
        SetUpTextEvent(GameResult.Instance.ResultTitleTMP.gameObject, //�^�C�g���֖߂�
                        (eventData) => { ChangeBlackText(GameResult.Instance.ResultTitleTMP); },
                        (eventData) => { ResetBlackText(GameResult.Instance.ResultTitleTMP); },
                        (eventData) => {
                            StartCoroutine(ChangeScene("Title"));
                        });
    }

    protected IEnumerator ChangeScene(string _stage)
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        fadeOutAnimator.SetBool("FadeOut",true);
        yield return new WaitForSeconds(fadeOutWaitTime);
        SceneManager.LoadScene(_stage);
    }
}
