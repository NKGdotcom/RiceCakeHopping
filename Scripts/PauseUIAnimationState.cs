using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIAnimationState : MonoBehaviour
{
    public static PauseUIAnimationState Instance { get; private set; }
    private SetClearConditions setClearConditions;

    private Animator pauseUIAnimator;
    private float fadeOutWaitTime = 0.5f;
    private int stageNum;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start()
    {
        pauseUIAnimator = GetComponent<Animator>();
        setClearConditions = GetComponent<SetClearConditions>();
        stageNum = setClearConditions.StageIndexNum;
    }

    public void OpenPause()
    {
        pauseUIAnimator.SetBool("DisplayUI", true);
    }
    public void ClosePause()
    {
        pauseUIAnimator.SetBool("DisplayUI", false);
    }
    public void PauseRetry()
    {
        StartCoroutine(ChangeScene($"Stage{stageNum}"));
    }
    public void PauseBackTitle()
    {
        StartCoroutine(ChangeScene("Title"));
    }

    private IEnumerator ChangeScene(string _stage)
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        pauseUIAnimator.SetBool("FadeOut", true);
        yield return new WaitForSeconds(fadeOutWaitTime);
        SceneManager.LoadScene(_stage);
    }
}
