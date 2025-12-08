using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [SerializeField] private ResultManager resultManager;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private TextMeshProUGUI stageDescriptionText;

    [SerializeField] private float textDisplayTime = 2;
    private float timer;
    private bool isGameActive;

    private void Start()
    {
        if(stageData != null)
        {
            timer = stageData.TimeLimit;
            stageDescriptionText.text = stageData.StageDescription;
            UpdateTimerText();
        }
        StartCoroutine(WaitTextDisplay());
    }

    private void Update()
    {
        if (!isGameActive) return;
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            isGameActive = false;
            resultManager.ShowResult(ResultType.NotEat);
        }
    }
    /// <summary>
    /// ゆっくりテキストを表示
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitTextDisplay()
    {
        yield return new WaitForSeconds(textDisplayTime);
        isGameActive = true;
        yield break;
    }
    /// <summary>
    /// タイマーのUI表示を更新するメソッド
    /// </summary>
    private void UpdateTimerText()
    {
        if (textMeshProUGUI == null) return;

        float currentTimer = Mathf.Max(0, timer);

        // 分と秒を計算
        int minutes = Mathf.FloorToInt(currentTimer / 60);
        int seconds = Mathf.FloorToInt(currentTimer % 60);

        // フォーマット指定
        textMeshProUGUI.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
    /// <summary>
    /// 餅ならこの処理から判定
    /// </summary>
    /// <param name="currentType"></param>
    /// <param name="currentSize"></param>
    public void CheckClearCondition(RicecakeObject ricecake)
    {
        if(!isGameActive) return;

        isGameActive = false;

        bool isTypeMatch = (ricecake.MyType == stageData.TargetRicecakeType); //味が一緒か
        bool isSizeMatch = (ricecake.RicecakeSize == stageData.TargetSize); //サイズが一緒か

        //リザルト結果を示す
        if(isTypeMatch && isSizeMatch)
        {
            resultManager.ShowResult(ResultType.VeryDelicious);
            return;
        }

        else if(!isTypeMatch && isSizeMatch)
        {
            resultManager.ShowResult(ResultType.SoDelicious);
            return;
        }

        else if (isTypeMatch && !isSizeMatch)
        {
            if(ricecake.RicecakeSize > stageData.TargetSize)
            {
                resultManager.ShowResult(ResultType.TooMuch);
                return;
            }
            else if(ricecake.RicecakeSize < stageData.TargetSize)
            {
                resultManager.ShowResult(ResultType.NotEnough);
                return;
            }
        }
    }

    /// <summary>
    /// 餅じゃない何かを食べた
    /// </summary>
    public void SomethingEat()
    {
        if (!isGameActive) return;
        isGameActive = false;
        resultManager.ShowResult(ResultType.SomethingGotIn);
        return;
    }
}