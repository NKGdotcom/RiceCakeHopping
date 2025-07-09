using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAnimationState : MonoBehaviour
{
    public static TitleAnimationState Instance { get; private set; }

    [SerializeField] private Animator titleAnimator;
    private float fadeOutSpeed = 0.5f;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    /// <summary>
    /// �^�C�g���̌�A�X�e�[�W�I�����V�ѕ��̃y�[�W����I��
    /// (�X�e�[�W�I�����V�ѕ��̃y�[�W�ɖ߂�ۂ��L��)
    /// </summary>
    public void MoveToTitleNext()
    {
        titleAnimator.SetTrigger("TapAnyKey");
        titleAnimator.SetBool("GoToStageSelect", false);
        titleAnimator.SetBool("GoToHowToPlay", false);
    }

    /// <summary>
    /// �X�e�[�W�I����ʂɈړ�
    /// </summary>
    public void MoveToStageSelectPage()
    {
        titleAnimator.SetBool("GoToStageSelect", true);
    }

    /// <summary>
    /// �V�ѕ��y�[�W�Ɉړ�
    /// </summary>
    public void MoveToHowToPlayPage()
    {
        titleAnimator.SetBool("GoToHowToPlay", true);
    }

    /// <summary>
    /// �Q�[���J�n����
    /// </summary>
    /// <param name="_stageName">�ڂ�X�e�[�W��</param>
    public void GameStart(string _stageName)
    {
        titleAnimator.SetTrigger("GameStart");
        StartCoroutine(WaitStart(_stageName));
    }

    /// <summary>
    /// �Q�[���J�n
    /// </summary>
    /// <param name="_stageName">�ڂ�X�e�[�W��</param>
    /// <returns></returns>
    private IEnumerator WaitStart(string _stageName)
    {
        yield return new WaitForSeconds(fadeOutSpeed);
        SceneManager.LoadScene(_stageName);
    }
}
