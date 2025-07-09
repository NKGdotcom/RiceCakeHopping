using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("�^�C�}�[")]
    [SerializeField] private TextMeshProUGUI timerText;
    [Header("���Ԑ���")]
    [SerializeField] private float timeLimit = 60;
    private float clearIntroduceTime = 2.5f;
    private const float oneMinute = 60;
    private float minutes;
    private float seconds;

    // Start is called before the first frame update
    void Start()
    {
        SetTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateMachine.Instance.IsIntroduceClear()) //�N���A������������
        {
            if(clearIntroduceTime > 0) clearIntroduceTime -=Time.deltaTime;
            else if(clearIntroduceTime < 0)
            {
                GameStateMachine.Instance.SetState(GameStateMachine.GameState.Playing);
                clearIntroduceTime = 0;
            }
        }
        else if (GameStateMachine.Instance.IsPlaying())
        {
            timeLimit -= Time.deltaTime;
            if (timeLimit > 0) SetTimerText();
            else if(timeLimit < 0)
            {
                GameStateMachine.Instance.SetState(GameStateMachine.GameState.Result);
                timerText.SetText("0:00");

                StartCoroutine(GameResult.Instance.NotEatRiceCake()); 
            }
        }
    }
    /// <summary>
    /// �^�C�}�[���e�L�X�g�ɓǂݍ���
    /// </summary>
    private void SetTimerText()
    {
        seconds = Mathf.Floor(Mathf.Repeat(timeLimit, oneMinute));
        minutes = Mathf.Floor(timeLimit / oneMinute);
        timerText.SetText(minutes.ToString("f0") + ":" + seconds.ToString("00"));
    }
}
