using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUIAnimationState : MonoBehaviour
{
    public static ResultUIAnimationState Instance { get; private set; }
    private SetClearConditions setClearConditions;

    private Animator resultUIAnimator;
    private float fadeOutWaitTime = 0.5f;
    private int stageNum;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start()
    {
        resultUIAnimator = GetComponent<Animator>();
        setClearConditions = GetComponent<SetClearConditions>();
        stageNum = setClearConditions.StageIndexNum;
    }
    public void ResultNextStage()
    {
        StartCoroutine(ChangeScene($"Stage{stageNum + 1}"));
    }
    public void ResultRetry()
    {
        StartCoroutine(ChangeScene($"Stage{stageNum}"));
    }
    public void ResultBackTitle()
    {
        StartCoroutine(ChangeScene("Title"));
    }

    private IEnumerator ChangeScene(string _stage)
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        resultUIAnimator.SetBool("FadeOut", true);
        yield return new WaitForSeconds(fadeOutWaitTime);
        SceneManager.LoadScene(_stage);
    }
}
