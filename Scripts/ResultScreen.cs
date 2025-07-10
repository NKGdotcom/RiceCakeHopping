using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���U���g���(UI�y�[�W���)
/// </summary>
public class ResultScreen : UIPageMove
{
    protected SetClearConditions setClearConditions;

    [Header("�t�F�[�h�A�E�g�p�̃A�j���[�V�����A��̂��͓̂���Ȃ��đ��v")]
    [SerializeField] protected Animator fadeOutAnimator;
    protected static readonly Color32 highlightColor = new Color32(255, 130, 130, 255);//�e�L�X�g�̏�ɒu�����班���Ԃ��ۂ��F��
    protected static readonly Color32 defaultColor = new Color32(0, 0, 0, 255);//�e�L�X�g����O�ꂽ��

    protected float stageNum;
    protected float fadeOutWaitTime = 0.5f;
    // Start is called before the first frame update
    new protected virtual void Start()
    {
        setClearConditions = GetComponent<SetClearConditions>();
        stageNum = setClearConditions.StageIndexNum;
        SetUpTextEvent(GameResult.Instance.ResultNextSceneTMP.gameObject, //���̃X�e�[�W��
                        (eventData) => { StartColorChange(GameResult.Instance.ResultNextSceneTMP, highlightColor); },
                        (eventData) => { StartColorChange(GameResult.Instance.ResultNextSceneTMP, defaultColor); },
                        (eventData) => {
                            StartCoroutine(ChangeScene($"Stage{stageNum + 1}"));
                        });
        SetUpTextEvent(GameResult.Instance.ResultOneMoreTimeTMP.gameObject, //������x�����X�e�[�W
                        (eventData) => { StartColorChange(GameResult.Instance.ResultOneMoreTimeTMP, highlightColor); },
                        (eventData) => { StartColorChange(GameResult.Instance.ResultOneMoreTimeTMP, defaultColor); },
                        (eventData) => {
                            StartCoroutine(ChangeScene($"Stage{stageNum}"));
                        });
        SetUpTextEvent(GameResult.Instance.ResultTitleTMP.gameObject, //�^�C�g���֖߂�
                        (eventData) => { StartColorChange(GameResult.Instance.ResultTitleTMP, highlightColor); },
                        (eventData) => { StartColorChange(GameResult.Instance.ResultTitleTMP, defaultColor); },
                        (eventData) => {
                            StartCoroutine(ChangeScene("Title"));
                        });
    }

    protected IEnumerator ChangeScene(string _stage)
    {
        fadeOutAnimator.SetBool("FadeOut",true);
        yield return new WaitForSeconds(fadeOutWaitTime);
        SceneManager.LoadScene(_stage);
    }
}
